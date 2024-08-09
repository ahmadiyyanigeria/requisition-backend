using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class RefundEntry
    {
        public Guid RefundEntryId { get; private set; }
        public Guid CashAdvanceId { get; private set; }
        public decimal Amount { get; private set; }
        public RefundStatus Status { get; private set; } = RefundStatus.Pending;
        public DateTime Date { get; private set; }
        public BankAccount? BankAccount { get; private set; }

        private RefundEntry() { }
        public RefundEntry(Guid cashAdvanceId, decimal amount)
        {
            RefundEntryId = Guid.NewGuid();
            CashAdvanceId = cashAdvanceId;
            Amount = amount;
            Date = DateTime.UtcNow;
        }

        public void SetRefundPaid(BankAccount account)
        {
            if(account is null)
            {
                throw new DomainException("Bank account details used to refund can not be null");
            }
            Status = RefundStatus.Paid;
            BankAccount = account;
        }
    }
}
