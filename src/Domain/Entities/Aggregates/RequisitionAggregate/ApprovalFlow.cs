namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class ApprovalFlow(Guid requisitionId, LinkedList<ApprovalStep> approvers)
    {
        public Guid ApprovalFlowId { get; private set; } = Guid.NewGuid();
        public Guid RequisitionId { get; private set; } = requisitionId;
        public LinkedList<ApprovalStep> Approvers { get; private set; } = approvers;
        public int CurrentStep { get; private set; } = 0;

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
    }
}
