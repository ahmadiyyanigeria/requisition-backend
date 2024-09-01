using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Enums;
using Domain.Paging;

namespace Domain.Repositories
{
    public interface ICashAdvanceRepository
    {
        Task<CashAdvance> AddAsync(CashAdvance cashAdvance);
        Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId);
        Task<CashAdvance> UpdateAsync(CashAdvance cashAdvance);
        Task<PaginatedList<CashAdvance>> GetCashAdvances(PageRequest pageRequest, bool usePaging = true, DateTime? requestedStartDate = null, DateTime? requestedEndDate = null, DateTime? disbursedStartDate = null, DateTime? disbursedEndDate = null, DateTime? retiredStartDate = null, DateTime? retiredEndDate = null, CashAdvanceStatus? status = null, decimal? minAdvanceAmount = null, decimal? maxAdvanceAmount = null);
    }
}
