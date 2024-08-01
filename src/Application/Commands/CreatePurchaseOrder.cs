using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands
{
    public  class CreatePurchaseOrder 
    {
        public class CreatePurchaseOrderCommand : IRequest<Guid>
        {
            public Guid RequisitionId { get; init; }
            public Guid VendorId { get; init; }  
        }

        public class Handler : IRequestHandler<CreatePurchaseOrderCommand, Guid>
        {
            private readonly IPurchaseOrderRepository _purchaseOrderRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRequisitionRepository _requisitionRepository;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork, IRequisitionRepository requisitionRepository)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
                _unitOfWork = unitOfWork;
                _requisitionRepository = requisitionRepository; 
            }

            public async Task<Guid> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                var requisition = await _requisitionRepository.GetByIdAsync(request.RequisitionId);
                if (requisition is null || requisition.Status != RequisitionStatus.Approved)
                {
                    throw new DomainException($"Requisition has not been approved.", ExceptionCodes.InvalidProcessingState.ToString(), 400);
                }
                var purchaseOrder = new PurchaseOrder(request.RequisitionId, request.VendorId);
                var purchaseOrderItems = requisition.Items.Select(r => new PurchaseOrderItem(r.Description, r.Quantity, r.UnitPrice, purchaseOrder.PurchaseOrderId));
                foreach ( var item in purchaseOrderItems )
                {
                    purchaseOrder.AddItem(item);
                }
                await _purchaseOrderRepository.AddAsync(purchaseOrder);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return purchaseOrder.PurchaseOrderId; 
            }
        }
    }
}
