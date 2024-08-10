using Domain.Entities.Aggregates.GrantAggregate;
using Domain.Entities.Common;

namespace Application.Repositories
{
    public interface IGrantRepository
    {
        Task<Grant> AddAsync(Grant grant);
        Task<Grant?> GetByIdAsync(Guid grantId);
        Task<IReadOnlyList<Grant>> GetAllAsync();
    }
}
