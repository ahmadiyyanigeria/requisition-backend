using Application.Paging;
using Domain.Entities.Aggregates.GrantAggregate;

namespace Application.Repositories
{
    public interface IGrantRepository
    {
        Task<Grant> AddAsync(Grant grant);
        Task<Grant?> GetByIdAsync(Guid grantId);

        Task<Grant> UpdateAsync(Grant grant);
        Task<PaginatedList<Grant>> GetGrants(PageRequest pageRequest, bool usePaging = true);
    }
}
