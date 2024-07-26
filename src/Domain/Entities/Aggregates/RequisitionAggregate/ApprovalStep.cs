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
        private readonly List<string> _approvalRoles = null!;

        public IReadOnlyList<string> ApprovalRoles => _approvalRoles.AsReadOnly();

        private ApprovalStep() { }

        public ApprovalStep(Guid approvalFlowId, string approverId, List<string> approverRoles)
        {
            ApprovalFlowId = approvalFlowId;
            ApproverId = approverId;
            _approvalRoles = approverRoles ?? [];
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
