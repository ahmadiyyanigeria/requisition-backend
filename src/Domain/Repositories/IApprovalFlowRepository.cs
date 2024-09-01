using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Domain.Repositories
{
    public interface IApprovalFlowRepository
    {
        Task<ApprovalFlow> AddAsync(ApprovalFlow approvalFlow);
        Task<ApprovalFlow?> GetByIdAsync(Guid approvalFlowId);       
    }
}
