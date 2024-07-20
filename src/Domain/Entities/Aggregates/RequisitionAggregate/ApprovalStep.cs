using Domain.Enums;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalStep
    {
        public Guid ApprovalStepId { get; private set; }
        public Guid ApproverId { get; private set; }
        public ApprovalFlow ApprovalFlow { get; private set; }
        public ApprovalStatus Status { get; private set; }
        public DateTime ApprovalDate { get; private set; }
        public string Notes { get; private set; }
        private readonly List<string> _approvalRoles;

        public IReadOnlyList<string> ApprovalRoles => _approvalRoles.AsReadOnly();

        private ApprovalStep() { }

        public ApprovalStep(Guid approverId, List<string> approverRoles)
        {
            ApprovalStepId = Guid.NewGuid();
            ApproverId = approverId;
            _approvalRoles = approverRoles ?? [];
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
