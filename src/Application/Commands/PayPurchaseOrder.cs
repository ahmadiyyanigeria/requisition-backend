using Application.Common.Interfaces;
using Domain.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public  class PayPurchaseOrder 
    {
        public class PayPurchaseOrderCommand : IRequest<PurchaseOrderResponse>
        {
            public Guid PurchaseOrderId { get; set; }
            public decimal Amount { get; init; }
            public PaymentMethod PaymentMethod { get; init; }
            public string ReferenceNumber { get; init; } = default!;
        }

        public class Handler : IRequestHandler<PayPurchaseOrderCommand, PurchaseOrderResponse>
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

            public async Task<PurchaseOrderResponse> Handle(PayPurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.PurchaseOrderId);

                if (purchaseOrder == null)
                {
                    throw new ApplicationException($"Purchase order not found.", ExceptionCodes.PurchaseOrderNotFound.ToString(), 404);
                }

                if (purchaseOrder.Status != PurchaseOrderStatus.Fulfilled)
                {
                    throw new ApplicationException($"Purchase order yet to be fulfilled.", ExceptionCodes.PurchaseOrderNotFulfilled.ToString(), 400);
                }

                if (purchaseOrder.Status == PurchaseOrderStatus.Paid)
                {
                    throw new ApplicationException($"Purchase order already paid.", ExceptionCodes.PurchaseOrderPaid.ToString(), 400);
                }

                var payment = new Payment(request.Amount, request.PaymentMethod, request.ReferenceNumber, request.PurchaseOrderId);
                purchaseOrder.AddPayment(payment);

                await _purchaseOrderRepository.AddPaymentAsync(payment);
                await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
                await _requisitionRepository.UpdateAsync(purchaseOrder.Requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new PurchaseOrderResponse(purchaseOrder.PurchaseOrderId, purchaseOrder.RequisitionId, purchaseOrder.OrderDate, purchaseOrder.TotalAmount, purchaseOrder.DeliveryDate, purchaseOrder.Status, purchaseOrder.Vendor, [.. purchaseOrder.Items], [.. purchaseOrder.Payments]);
                return response;
            }
        }

        public record PurchaseOrderResponse(Guid PurchaseOrderId, Guid RequisitionId, DateTime OrderDate, decimal TotalAmount, DateTime? DeliveryDate, PurchaseOrderStatus Status, Vendor Vendor, List<PurchaseOrderItem> Items, List<Payment> Payments);
    }
}
