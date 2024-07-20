namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalFlow
    {
        public Guid ApprovalFlowId { get; private set; } = Guid.NewGuid();
        public Guid RequisitionId { get; private set; }
        public LinkedList<ApprovalStep> Approvers { get; private set; }
        public int CurrentStep { get; private set; } = 0;

        private ApprovalFlow() { }

        public ApprovalFlow(Guid requisitionId, LinkedList<ApprovalStep> approvers)
        {
            RequisitionId = requisitionId;
            Approvers = approvers;
        }

        public void MoveToNextStep()
        {
            if (CurrentStep < Approvers.Count - 1)
            {
                CurrentStep++;
            }
        }

        public ApprovalStep GetCurrentApprover()
        {
            return Approvers.ElementAt(CurrentStep);
        }

        public bool IsFinalStep()
        {
            return CurrentStep >= Approvers.Count - 1;
        }
    }
}
