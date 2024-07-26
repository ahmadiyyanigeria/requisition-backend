using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ExpenseHeadRepository : IExpenseHeadRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseHeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExpenseHead> AddAsync(ExpenseHead expenseHead)
        {
            await _context.AddAsync(expenseHead);
            return expenseHead;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.ExpenseHeads.AnyAsync(n => n.Name == name);
        }

        public async Task<IReadOnlyList<ExpenseHead>> GetAllAsync()
        {
           return await _context.ExpenseHeads.ToListAsync();
        }

        public async Task<ExpenseHead?> GetByNameAsync(string name)
        {
            return await _context.ExpenseHeads.SingleOrDefaultAsync(n => n.Name == name);
        }
    }
}
