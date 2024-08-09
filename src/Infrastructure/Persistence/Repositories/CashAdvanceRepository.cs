using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId)
        {
            return await _context.CashAdvances.FirstOrDefaultAsync(ca => ca.CashAdvanceId == cashAdvanceId);
        }

        public async Task<PaginatedList<CashAdvance>> GetCashAdvances(PageRequest pageRequest, bool usePaging = true, DateTime? requestedStartDate = null, DateTime? requestedEndDate = null, DateTime? disbursedStartDate = null, DateTime? disbursedEndDate = null, DateTime? retiredStartDate = null, DateTime? retiredEndDate = null, CashAdvanceStatus? status = null, decimal? minAdvanceAmount = null, decimal? maxAdvanceAmount = null)
        {
            var query = _context.CashAdvances.Include(x => x.RetirementEntry).Include(x => x.RetirementEntry).Include(x => x.ReimbursementEntry).AsQueryable();
            if (requestedStartDate.HasValue)
            {
                query = query.Where(x => x.RequestedDate >= requestedStartDate.Value);
            }
            if (requestedEndDate.HasValue)
            {
                query = query.Where(x => x.RequestedDate <= requestedEndDate.Value);
            }
            if (disbursedStartDate.HasValue)
            {
                query = query.Where(x => x.DisbursedDate >= disbursedStartDate.Value);
            }
            if (disbursedEndDate.HasValue)
            {
                query = query.Where(x => x.DisbursedDate <= disbursedEndDate.Value);
            }
            if (retiredStartDate.HasValue)
            {
                query = query.Where(x => x.RetiredDate >= retiredStartDate.Value);
            }
            if (retiredEndDate.HasValue)
            {
                query = query.Where(x => x.RetiredDate <= retiredEndDate.Value);
            }
            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }
            if (minAdvanceAmount.HasValue)
            {
                query = query.Where(x => x.AdvanceAmount >= minAdvanceAmount.Value);
            }
            if (maxAdvanceAmount.HasValue)
            {
                query = query.Where(x => x.AdvanceAmount <= maxAdvanceAmount.Value);
            }

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<CashAdvance>(_context);
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
