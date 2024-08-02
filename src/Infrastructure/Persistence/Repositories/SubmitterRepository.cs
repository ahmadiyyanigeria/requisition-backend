using Application.Paging;
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

        public async Task<Submitter?> GetByUserIdAsync(string userId)
        {
            return await _context.Submitters.FirstOrDefaultAsync(g => g.UserId == userId);
        }

        public async Task<Submitter?> GetByIdAsync(Guid submitterId)
        {
            return await _context.Submitters.FirstOrDefaultAsync(g => g.SubmitterId == submitterId);
        }

        public async Task<PaginatedList<Submitter>> GetSubmitters(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Submitters.AsQueryable();

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Submitter>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderBy(r => r.Name);

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }
    }
}
