using Application.Paging;
using Domain.Entities.Aggregates.SubmitterAggregate;

namespace Application.Repositories
{
    public interface ISubmitterRepository
    {
        Task<Submitter?> GetByIdAsync(Guid submitterId);
        Task<Submitter?> GetByUserIdAsync(string userId);
        Task<Submitter?> GetByEmailAsync(string email);
        Task<Submitter> AddAsync(Submitter submitter);
        Task<PaginatedList<Submitter>> GetSubmitters(PageRequest pageRequest, bool usePaging = true);
    }
}
