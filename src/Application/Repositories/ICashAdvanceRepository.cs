using Application.Paging;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface ICashAdvanceRepository
    {
        Task<CashAdvance> AddAsync(CashAdvance cashAdvance);
        Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId);
        Task<PaginatedList<CashAdvance>> GetAll(PageRequest pageRequest, Expression<Func<CashAdvance, bool>> predicate, bool usePaging);
    }
}
