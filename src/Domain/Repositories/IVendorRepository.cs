using Domain.Paging;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;

namespace Domain.Repositories
{
    public interface IVendorRepository
    {
        Task<Vendor> AddAsync(Vendor vendor);
        Task<PaginatedList<Vendor>> GetVendors(PageRequest pageRequest, bool usePaging = true);
        Task<Vendor?> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(string name);
    }
}