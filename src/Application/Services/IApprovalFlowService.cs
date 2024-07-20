using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;

namespace Application.Services
{
    public interface IApprovalFlowService
    {
        ApprovalFlow CreateApprovalFlow(Requisition requisition, string submitterRole);
    }
}
