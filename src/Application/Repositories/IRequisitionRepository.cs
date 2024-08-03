using Application.Paging;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;

namespace Application.Repositories
{
    public interface IRequisitionRepository
    {
        Task<Requisition> AddAsync(Requisition requisition);
        Task<Requisition> UpdateAsync(Requisition requisition);
        Task<Requisition?> GetByIdAsync(Guid requisitionId);
        Task<IReadOnlyList<Requisition>> GetAllAsync();
        Task<IReadOnlyList<Requisition>> GetAllAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<PaginatedList<Requisition>> GetPaginatedAsync(PageRequest pageRequest, DateTime? startDate, DateTime? endDate, string? expenseHead, string? Department, HashSet<RequisitionStatus>? StatusFilter, HashSet<RequisitionType>? TypeFilter);
        Task<IReadOnlyList<Requisition>> GetRequisitionsBySubmitterAsync(Guid submitterId);
    }
}
