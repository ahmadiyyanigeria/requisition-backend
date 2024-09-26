using Domain.Paging;
using Domain.Entities.Aggregates.SubmitterAggregate;

namespace Domain.Repositories
{
    public interface ISubmitterRepository
    {
        Task<Submitter?> GetByIdAsync(Guid submitterId);
        Task<IReadOnlyList<Submitter?>> GetByUserIdAsync(string userId);
        Task<Submitter?> GetByEmailAsync(string email);
        Task<Submitter> AddAsync(Submitter submitter);
        Task<PaginatedList<Submitter>> GetSubmitters(PageRequest pageRequest, bool usePaging = true);
    }
}
