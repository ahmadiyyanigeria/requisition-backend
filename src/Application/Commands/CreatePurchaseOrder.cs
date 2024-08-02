using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.Common;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

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
            private readonly ICurrentUser _user;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork, IRequisitionRepository requisitionRepository, ICurrentUser user)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
                _unitOfWork = unitOfWork;
                _requisitionRepository = requisitionRepository; 
                _user = user;
            }

            public async Task<Guid> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                var user = _user.GetUserDetails();
                string department = "Account";

                //create submitter record
                var submitter = new Submitter(user.UserId, user.Name, user.Email, user.Role, department);

                var requisition = await _requisitionRepository.GetByIdAsync(request.RequisitionId);
                if (requisition is null || requisition.Status != RequisitionStatus.Approved)
                {
                    throw new ApplicationException($"Requisition has not been approved.", ExceptionCodes.InvalidProcessingState.ToString(), 400);
                }
                var purchaseOrder = new PurchaseOrder(request.RequisitionId, request.VendorId, submitter.SubmitterId);
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
