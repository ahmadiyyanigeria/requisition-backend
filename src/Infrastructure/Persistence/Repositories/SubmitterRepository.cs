using Application.Repositories;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SubmitterRepository : ISubmitterRepository
    {
        private readonly ApplicationDbContext _context;

        public SubmitterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Submitter> AddAsync(Submitter submitter)
        {
            await _context.Submitters.AddAsync(submitter);
            return submitter;
        }
        public async Task<Submitter?> GetByEmailAsync(string email)
        {
            return await _context.Submitters.FirstOrDefaultAsync(g => g.Email == email);
        }

        public Task<Submitter?> GetByIdAsync(Guid submitterId)
        {
            throw new NotImplementedException();
        }
    }
}
