using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class CashAdvance
    {
        public Guid CashAdvanceId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public decimal AdvanceAmount { get; private set; }
        public BankAccount BankAccount { get; private set; } = default!;
        public CashAdvanceStatus Status { get; private set; } = CashAdvanceStatus.Requested;
        public DateTime RequestedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? DisbursedDate { get; private set; } 
        public DateTime? RetiredDate { get; private set; } 
        public RetirementEntry? RetirementEntry { get; private set; }
        public RefundEntry? RefundEntry { get; private set; }
        public ReimbursementEntry? ReimbursementEntry { get; private set; }

        private CashAdvance() { }
        public CashAdvance(Guid requisitionId, Guid submitterId, decimal advanceAmount, BankAccount bankAccount)
        {
            CashAdvanceId = Guid.NewGuid();
            RequisitionId = requisitionId;
            SubmitterId = submitterId;
            AdvanceAmount = advanceAmount;
            BankAccount = bankAccount;
        }

        public void Disburse()
        {
            if (Status == CashAdvanceStatus.Requested)
            {
                Status = CashAdvanceStatus.Disbursed;
                DisbursedDate = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException($"Cash advance is not in requested state.");
            }
        }

        public void Retire(RetirementEntry retirementEntry)
        {
            if (Status == CashAdvanceStatus.Disbursed)
            {
                RetirementEntry = retirementEntry;
                Status = CashAdvanceStatus.Retired;
                RetiredDate = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException($"Cash advance is not in disbursed state.");
            }
        }

        public void AddRefundEntry(RefundEntry refundEntry)
        {
            if (Status == CashAdvanceStatus.Retired)
            {
                RefundEntry = refundEntry;
            }
            else
            {
                throw new DomainException($"Cash advance is not in retired state.");
            }
        }

        public void AddReimbursementEntry(ReimbursementEntry reimbursementEntry)
        {
            if (Status == CashAdvanceStatus.Retired)
            {
                ReimbursementEntry = reimbursementEntry;
            }
            else
            {
                throw new DomainException($"Cash advance is not in retired state.");
            }
        }
    }
}
