using Domain.Entities.Aggregates.GrantAggregate;

namespace Application.Repositories
{
    public interface IGrantRepository
    {
        Task<Grant> AddAsync(Grant grant);
        Task<Grant?> GetByIdAsync(Guid grantId);
    }
}
