using Domain.Entities.Aggregates.CashAdvanceAggregate;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface ICashAdvanceRepository
    {
        Task<CashAdvance> AddAsync(CashAdvance cashAdvance);
        Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId);
        Task<CashAdvance?> GetByIdAsync(Expression<Func<CashAdvance, bool>> expression);
    }
}
