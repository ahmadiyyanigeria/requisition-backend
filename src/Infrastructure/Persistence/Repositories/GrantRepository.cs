using Application.Repositories;
using Domain.Entities.Aggregates.GrantAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GrantRepository : IGrantRepository
    {
        private readonly ApplicationDbContext _context;

        public GrantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Grant> AddAsync(Grant grant)
        {
            await _context.Grants.AddAsync(grant);
            return grant;
        }

        public Task<Grant?> GetByIdAsync(Guid grantId)
        {
            return _context.Grants.FirstOrDefaultAsync(g => g.GrantId == grantId);
        }
    }
}
