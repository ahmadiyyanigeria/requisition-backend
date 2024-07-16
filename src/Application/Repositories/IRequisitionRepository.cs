using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Application.Repositories
{
    public interface IRequisitionRepository
    {
        Task<Requisition> AddAsync(Requisition requisition);
        Requisition Update(Requisition requisition);
        Task<Requisition?> GetByIdAsync(Guid requisitionId);
        Task<IReadOnlyList<Requisition>> GetAllAsync();
        Task<IReadOnlyList<Requisition>> GetRequisitionsBySubmitterAsync(Guid submitterId);
    }
}
