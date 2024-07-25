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

        public void MoveToNextStep()
        {
            if (CurrentStep < ApproverSteps.Count - 1)
            {
                CurrentStep++;
            }
        }

        public ApprovalStep GetCurrentApprover()
        {
            return ApproverSteps.ElementAt(CurrentStep);
        }

        public bool IsFinalStep()
        {
            return CurrentStep >= ApproverSteps.Count - 1;
        }
    }
}
