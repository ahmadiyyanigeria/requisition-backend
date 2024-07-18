using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Application.Repositories
{
    public interface IApprovalFlowRepository
    {
        Task<ApprovalFlow> AddAsync(ApprovalFlow approvalFlow);
        Task<ApprovalFlow?> GetByIdAsync(Guid approvalFlowId);       
    }
}
