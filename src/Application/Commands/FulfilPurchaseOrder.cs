using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public  class FulfilPurchaseOrder 
    {
        public class FulfilPurchaseOrderCommand : IRequest<PurchaseOrderResponse>
        {
            public Guid PurchaseOrderId { get; set; }
        }

        public class Handler : IRequestHandler<FulfilPurchaseOrderCommand, PurchaseOrderResponse>
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

            public async Task<PurchaseOrderResponse> Handle(FulfilPurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.PurchaseOrderId);

                if (purchaseOrder == null)
                {
                    throw new ApplicationException($"Purchase order not found.", ExceptionCodes.PurchaseOrderNotFound.ToString(), 404);
                }

                if (purchaseOrder.Status != PurchaseOrderStatus.Requested)
                {
                    throw new ApplicationException($"Purchase order not in request status.", ExceptionCodes.PurchaseOrderNotInRequestState.ToString(), 400);
                }

                if (purchaseOrder.Status == PurchaseOrderStatus.Paid)
                {
                    throw new ApplicationException($"Purchase order already paid.", ExceptionCodes.PurchaseOrderPaid.ToString(), 400);
                }

                purchaseOrder.Fulfill();

                await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = new PurchaseOrderResponse(purchaseOrder.PurchaseOrderId, purchaseOrder.RequisitionId, purchaseOrder.OrderDate, purchaseOrder.TotalAmount, purchaseOrder.DeliveryDate, purchaseOrder.Status, purchaseOrder.Vendor, [.. purchaseOrder.Items], [.. purchaseOrder.Payments]);
                return response;
            }
        }

        public record PurchaseOrderResponse(Guid PurchaseOrderId, Guid RequisitionId, DateTime OrderDate, decimal TotalAmount, DateTime? DeliveryDate, PurchaseOrderStatus Status, Vendor Vendor, List<PurchaseOrderItem> Items, List<Payment> Payments);
    }
}
