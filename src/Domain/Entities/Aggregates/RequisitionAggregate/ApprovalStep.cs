using Domain.Entities.Common;
using Domain.Enums;
using System.Data;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalStep
    {
        public Guid ApprovalStepId { get; private set; }
        public Guid ApproverId { get; private set; }
        public ApprovalFlow ApprovalFlow { get; private set; }
        public List<Role> ApproverRole { get; private set; }
        public ApprovalStatus Status { get; private set; }
        public DateTime ApprovalDate { get; private set; }
        public string Notes { get; private set; }

        public ApprovalStep(Guid approverId, List<Role> approverRole)
        {
            ApprovalStepId = Guid.NewGuid();
            ApproverId = approverId;
            ApproverRole = approverRole;
            Status = ApprovalStatus.Pending;
        }

        public void Approve(string notes)
        {
            Status = ApprovalStatus.Approved;
            ApprovalDate = DateTime.UtcNow;
            Notes = notes;
        }

        public void Reject(string notes)
        {
            Status = ApprovalStatus.Rejected;
            ApprovalDate = DateTime.UtcNow;
            Notes = notes;
        }
    }
}
