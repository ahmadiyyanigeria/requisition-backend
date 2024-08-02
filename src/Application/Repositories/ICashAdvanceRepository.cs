using Application.Paging;
using Domain.Entities.Aggregates.CashAdvanceAggregate;

namespace Application.Repositories
{
    public interface ICashAdvanceRepository
    {
        Task<CashAdvance> AddAsync(CashAdvance cashAdvance);
        Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId);
        Task<PaginatedList<CashAdvance>> GetCashAdvances(PageRequest pageRequest, bool usePaging = true);
    }
}
