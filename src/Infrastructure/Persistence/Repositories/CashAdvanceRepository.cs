using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
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
    }
}
