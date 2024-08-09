using Application.Paging;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Enums;

namespace Application.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId);
        Task<PaginatedList<PurchaseOrder>> GetPurchaseOrders(PageRequest pageRequest, bool usePaging = true, DateTime? orderStartDate = null, DateTime? orderEndDate = null, DateTime? deliveryStartDate = null, DateTime? deliveryEndDate = null, decimal? minTotalAmount = null, decimal? maxTotalAmount = null, PurchaseOrderStatus? status = null, Guid? vendorId = null);
    }
}
