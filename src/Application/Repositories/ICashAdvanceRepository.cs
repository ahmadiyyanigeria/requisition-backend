using Domain.Entities.Aggregates.CashAdvanceAggregate;

namespace Application.Repositories
{
    public interface ICashAdvanceRepository
    {
        Task<CashAdvance> AddAsync(CashAdvance cashAdvance);
        Task<CashAdvance?> GetByIdAsync(Guid cashAdvanceId);       
    }
}
