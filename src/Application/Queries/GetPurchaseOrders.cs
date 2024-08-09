using Application.Paging;
using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetPurchaseOrders
    {
        public record Query(bool UsePaging = true,
        DateTime? OrderStartDate = null,
        DateTime? OrderEndDate = null,
        DateTime? DeliveryStartDate = null,
        DateTime? DeliveryEndDate = null,
        decimal? MinTotalAmount = null,
        decimal? MaxTotalAmount = null,
        PurchaseOrderStatus? Status = null,
        Guid? VendorId = null) : PageRequest, IRequest<PaginatedList<PurchaseOrderResponse>>;

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
                var purchaseOrders = await _purchaseOrderRepository.GetPurchaseOrders(request, request.UsePaging, request.OrderStartDate, request.OrderEndDate, request.DeliveryStartDate, request.DeliveryEndDate, request.MinTotalAmount, request.MaxTotalAmount, request.Status, request.VendorId
            );
                return purchaseOrders.Adapt<PaginatedList<PurchaseOrderResponse>>();
            }
        }
    }
}
