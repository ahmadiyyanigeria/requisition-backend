using Application.Paging;
using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetPurchaseOrders
    {
        public record Query : PageRequest, IRequest<PaginatedList<PurchaseOrderResponse>>
        {
            public bool UsePaging { get; init; } = true;
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

        public class Handler : IRequestHandler<Query, PaginatedList<PurchaseOrderResponse>>
        {
            private readonly IPurchaseOrderRepository _purchaseOrderRepository;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
            }
            public async Task<PaginatedList<PurchaseOrderResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var purchaseOrders = await _purchaseOrderRepository.GetPurchaseOrders(request, request.UsePaging);
                return purchaseOrders.Adapt<PaginatedList<PurchaseOrderResponse>>();
            }
        }
    }
}
