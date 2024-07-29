using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class CashAdvanceRepository : ICashAdvanceRepository
    {
        private readonly ApplicationDbContext _context;

        public CashAdvanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CashAdvance> AddAsync(CashAdvance cashAdvance)
        {
            await _context.CashAdvances.AddAsync(cashAdvance);
            return cashAdvance;
        }

        public async Task<PaginatedList<CashAdvance>> GetAll(PageRequest pageRequest, Expression<Func<CashAdvance, bool>> predicate, bool usePaging)
        {
            var query = _context.CashAdvances.Where(predicate);

            if (pageRequest.IsAscending)
            {
                query = query.OrderBy(ca => ca.CashAdvanceId); 
            }
            else
            {
                query = query.OrderByDescending(ca => ca.CashAdvanceId);
            }

            var totalCount = await query.CountAsync();

            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalCount, 1, totalCount);
            }
        }

        public async Task<List<Guid>> GetAllCashAdvanceIds()
        {
            return await _context.CashAdvances.Select(ca => ca.CashAdvanceId).ToListAsync();
        }

        public async Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId)
        {
            return await _context.CashAdvances.FirstOrDefaultAsync(ca => ca.CashAdvanceId == cashAdvanceId);
        }

    }
}