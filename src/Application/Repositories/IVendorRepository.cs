using Domain.Entities.Aggregates.PurchaseOrderAggregate;

namespace Application.Repositories
{
    public interface IVendorRepository
    {
        Task<Vendor> AddAsync(Vendor vendor);
        Task<IReadOnlyList<Vendor>> GetAllAsync();
        Task<Vendor?> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(string name);
    }
}