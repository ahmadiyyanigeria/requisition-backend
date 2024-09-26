using Domain.Paging;
using Domain.Repositories;
using Domain.Enums;
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

        public record PurchaseOrderResponse(Guid PurchaseOrderId, Guid RequisitionId, DateTime OrderDate, decimal TotalAmount, DateTime? DeliveryDate, PurchaseOrderStatus Status, string Vendor);

        public class Handler : IRequestHandler<Query, PaginatedList<PurchaseOrderResponse>>
        {
            private readonly IPurchaseOrderRepository _purchaseOrderRepository;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
            }
            public async Task<PaginatedList<PurchaseOrderResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var purchaseOrders = await _purchaseOrderRepository.GetPurchaseOrders(request, request.UsePaging, request.OrderStartDate, request.OrderEndDate, request.DeliveryStartDate, request.DeliveryEndDate, request.MinTotalAmount, request.MaxTotalAmount, request.Status, request.VendorId);

                var purchaseOrderResponses = purchaseOrders.Items.Select(po => new PurchaseOrderResponse(po.PurchaseOrderId,po.RequisitionId,po.OrderDate,po.TotalAmount,po.DeliveryDate,po.Status, po.Vendor.Name)).ToList();

                var response = new PaginatedList<PurchaseOrderResponse>
                {
                    Items = purchaseOrderResponses,
                    PageSize = purchaseOrders.PageSize,
                    TotalItems = purchaseOrders.TotalItems,
                    Page = purchaseOrders.Page
                };

                return response;
            }
        }
    }
}
