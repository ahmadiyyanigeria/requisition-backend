using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Entities.Aggregates.GrantAggregate
{
    public class Grant
    {
        public Guid GrantId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public decimal GrantAmount { get; private set; }
        //public string AccountNumber { get; private set; }
       // public BankAccount BankAccount { get; private set; }
        public GrantStatus Status { get; private set; }

        private Grant() { }

        public Grant(Guid requisitionId, Guid submitterId, decimal grantAmount)
        {
            GrantId = Guid.NewGuid();
            RequisitionId = requisitionId;
            SubmitterId = submitterId;
            GrantAmount = grantAmount;
            //AccountNumber = bankAccount;
            Status = GrantStatus.Requested;
        }

        public void Approve()
        {
            if (Status == GrantStatus.Requested)
            {
                Status = GrantStatus.Approved;
            }
            // Consider throwing an exception or handling other cases
        }

        public void Disburse()
        {
            if (Status == GrantStatus.Approved)
            {
                Status = GrantStatus.Disbursed;
            }
            // Consider throwing an exception or handling other cases
        }
    }
}
