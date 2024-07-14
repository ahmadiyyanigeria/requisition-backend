using Domain.Entities.Common;
using Domain.Enums;
using System;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class CashAdvance
    {
        public Guid CashAdvanceId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public decimal AdvanceAmount { get; private set; }
        public string AccountNumber { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public CashAdvanceStatus Status { get; private set; }
        public RetirementEntry RetirementEntry { get; private set; }
        public RefundEntry RefundEntry { get; private set; }
        public ReimbursementEntry ReimbursementEntry { get; private set; }

        private CashAdvance() { }
        public CashAdvance(Guid requisitionId, Guid submitterId, decimal advanceAmount, string accountNumber)
        {
            CashAdvanceId = Guid.NewGuid();
            RequisitionId = requisitionId;
            SubmitterId = submitterId;
            AdvanceAmount = advanceAmount;
            AccountNumber = accountNumber;
            Status = CashAdvanceStatus.Requested;
        }

        public void Approve()
        {
            if (Status == CashAdvanceStatus.Requested)
            {
                Status = CashAdvanceStatus.Approved;
            }
            // Consider throwing an exception or handling other cases
        }

        public void Disburse()
        {
            if (Status == CashAdvanceStatus.Approved)
            {
                Status = CashAdvanceStatus.Disbursed;
            }
            // Consider throwing an exception or handling other cases
        }

        public void Retire(RetirementEntry retirementEntry)
        {
            if (Status == CashAdvanceStatus.Disbursed)
            {
                RetirementEntry = retirementEntry;
                Status = CashAdvanceStatus.Retired;
            }
            // Consider throwing an exception or handling other cases
        }

        public void AddRefundEntry(RefundEntry refundEntry)
        {
            if (Status == CashAdvanceStatus.Retired)
            {
                RefundEntry = refundEntry;
            }
            // Consider throwing an exception or handling other cases
        }

        public void AddReimbursementEntry(ReimbursementEntry reimbursementEntry)
        {
            if (Status == CashAdvanceStatus.Retired)
            {
                ReimbursementEntry = reimbursementEntry;
            }
            // Consider throwing an exception or handling other cases
        }
    }
}
