namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalFlow
    {
        public Guid ApprovalFlowId { get; private set; } = Guid.NewGuid();
        public Guid RequisitionId { get; private set; }
        public LinkedList<ApprovalStep> ApproverSteps { get; private set; } = null!;
        public int CurrentStep { get; private set; } = 0;

        private ApprovalFlow() { }

        public ApprovalFlow(Guid requisitionId, LinkedList<ApprovalStep> approverSteps)
        {
            RequisitionId = requisitionId;
            ApproverSteps = approverSteps;
        }

        public void MoveToNextStep(string approverId)
        {
            var currentApprover = GetApprover(approverId);
            var currentApproverOrder = currentApprover.Order;
            currentApproverOrder++;
            if (CurrentStep < ApproverSteps.Count - 1)
            {
                CurrentStep = currentApproverOrder;
            }
        }

        public ApprovalStep GetCurrentApprover()
        {
            return ApproverSteps.ElementAt(CurrentStep);
        }

        public ApprovalStep? GetApprover(string approverId)
        {
            return ApproverSteps.FirstOrDefault(a => a.ApproverId == approverId);
        }

        public bool CanApprove(string approverId)
        {
            var currentApprover = GetApprover(approverId);
            var currentApproverOrder = currentApprover.Order;

            // Check if the approver has a higher order than the current step
            var approver = ApproverSteps.FirstOrDefault(a => a.ApproverId == approverId);
            return approver != null && approver.Order >= currentApproverOrder;
        }

        public bool IsFinalStep(string approverId)
        {
            var currentApprover = GetApprover(approverId);
            var currentApproverOrder = currentApprover.Order;
            CurrentStep = currentApproverOrder;
            return CurrentStep >= ApproverSteps.Count - 1;
        }
    }
}
