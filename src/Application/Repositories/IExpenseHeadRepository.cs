using Domain.Entities.Common;

namespace Application.Repositories
{
    public interface IExpenseHeadRepository
    {
        Task<ExpenseHead> AddAsync(ExpenseHead expenseHead);
        Task<IReadOnlyList<ExpenseHead>> GetAllAsync();
        Task<ExpenseHead?> GetByNameAsync(string name);
        Task<bool> ExistAsync(string name);
    }
}