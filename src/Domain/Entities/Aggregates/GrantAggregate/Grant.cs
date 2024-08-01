using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Aggregates.GrantAggregate
{
    public class Grant
    {
        public Guid GrantId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public decimal GrantAmount { get; private set; }
        public DateTime RequestedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? DisbursedDate { get; private set; }
        public BankAccount BankAccount { get; private set; } = default!;
        public GrantStatus Status { get; private set; } = GrantStatus.Requested;

        private Grant() { }

        public Grant(Guid requisitionId, Guid submitterId, decimal grantAmount, BankAccount bankAccount)
        {
            GrantId = Guid.NewGuid();
            RequisitionId = requisitionId;
            SubmitterId = submitterId;
            GrantAmount = grantAmount;
            BankAccount = bankAccount;
        }

        public void Disburse()
        {
            if (Status == GrantStatus.Requested)
            {
                Status = GrantStatus.Disbursed;
                DisbursedDate = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException($"Grant is not in requested state.");
            }
        }
    }
}
