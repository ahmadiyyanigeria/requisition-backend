using Application.Paging;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;

namespace Application.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId);
        Task<PaginatedList<PurchaseOrder>> GetPurchaseOrders(PageRequest pageRequest, bool usePaging = true);
    }
}
