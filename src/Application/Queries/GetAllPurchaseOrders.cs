using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetAllPurchaseOrders
    {
        public record Query : IRequest<List<PurchaseOrderResponse>>
        {

        }

        public record PurchaseOrderResponse
        {
            public Guid PurchaseOrderId { get; private set; }
            public Guid RequisitionId { get; private set; }
            public string Vendor { get; private set; } = default!;
            public DateTime OrderDate { get; private set; }
            public DateTime? DeliveryDate { get; private set; }
            public decimal TotalAmount { get; private set; }
            public PurchaseOrderStatus Status { get; private set; }
        }

        public class Handler : IRequestHandler<Query, List<PurchaseOrderResponse>>
        {
            private readonly IPurchaseOrderRepository _purchaseOrderRepository;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
            }
            public async Task<List<PurchaseOrderResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var purchaseOrders = await _purchaseOrderRepository.GetAllPurchaseOrders();
                return purchaseOrders.Adapt<List<PurchaseOrderResponse>>();
            }
        }
    }
}
