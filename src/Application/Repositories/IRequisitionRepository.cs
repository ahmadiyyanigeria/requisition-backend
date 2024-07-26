using Application.Paging;
using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Application.Repositories
{
    public interface IRequisitionRepository
    {
        Task<Requisition> AddAsync(Requisition requisition);
        Task<Requisition> UpdateAsync(Requisition requisition);
        Task<Requisition?> GetByIdAsync(Guid requisitionId);
        Task<IReadOnlyList<Requisition>> GetAllAsync();
        Task<PaginatedList<Requisition>> GetAllAsync(PageRequest pageRequest, DateTime? startDate, DateTime? endDate, string? expenseHead, string? Department, HashSet<int>? StatusFilter, HashSet<int>? TypeFilter);
        Task<IReadOnlyList<Requisition>> GetRequisitionsBySubmitterAsync(Guid submitterId);
    }
}
