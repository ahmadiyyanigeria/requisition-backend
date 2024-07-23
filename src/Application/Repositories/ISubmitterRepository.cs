using Domain.Entities.Aggregates.SubmitterAggregate;

namespace Application.Repositories
{
    public interface ISubmitterRepository
    {
        Task<Submitter?> GetByIdAsync(Guid submitterId);
        Task<Submitter?> GetByEmailAsync(string email);
        Task<Submitter> AddAsync(Submitter submitter);
    }
}
