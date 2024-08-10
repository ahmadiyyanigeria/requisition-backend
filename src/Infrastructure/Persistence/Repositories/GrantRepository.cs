using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.GrantAggregate;
using Domain.Entities.Aggregates.RequisitionAggregate;
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

        public async Task<Grant?> GetByIdAsync(Guid grantId)
        {
            return await _context.Grants.FirstOrDefaultAsync(g => g.GrantId == grantId);
        }

        public async Task<Grant> UpdateAsync(Grant grant)
        {
            _context.Grants.Update(grant);
            return grant;
        }

        public async Task<PaginatedList<Grant>> GetGrants(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Grants.AsQueryable();

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Grant>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderBy(r => r.RequestedDate);

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
