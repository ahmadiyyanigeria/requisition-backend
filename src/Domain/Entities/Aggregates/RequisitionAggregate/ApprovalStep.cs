using Domain.Enums;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalStep
    {
        public Guid ApprovalStepId { get; private set; } = Guid.NewGuid();
        public string ApproverId { get; private set; } = default!;
        public Guid ApprovalFlowId { get; private set; }
        public ApprovalStatus Status { get; private set; } = ApprovalStatus.Pending;
        public DateTime ApprovalDate { get; private set; }
        public string? Notes { get; private set; }
        public int Order { get; private set; }
        public string Role { get; private set; } = default!;

        private ApprovalStep() { }

        public ApprovalStep(Guid approvalFlowId, string approverId, string role, int order)
        {
            ApprovalFlowId = approvalFlowId;
            ApproverId = approverId;
            Order = order;
            Role = role;
        }

        public void Approve(string? notes)
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
