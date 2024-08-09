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
        Task<PaginatedList<Requisition>> GetRequisitions(PageRequest pageRequest, bool usePaging = true, DateTime? requestedStartDate = null, DateTime? requestedEndDate = null, RequisitionStatus? status = null, decimal? minTotalAmount = null, decimal? maxTotalAmount = null, IReadOnlyList<Guid>? submitterIds = null, string? expenseHead = null, RequisitionType? requisitionType = null, string? department = null);
        Task<IReadOnlyList<Requisition>> GetRequisitionsBySubmitterAsync(Guid submitterId);
    }
}
