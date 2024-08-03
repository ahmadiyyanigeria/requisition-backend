using Domain.Entities.ValueObjects;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class RefundEntry
    {
        public Guid RefundEntryId { get; private set; }
        public Guid CashAdvanceId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public BankAccount BankAccount { get; private set; } = default!;

        private RefundEntry() { }
        public RefundEntry(Guid cashAdvanceId, decimal amount, BankAccount bankAccount)
        {
            RefundEntryId = Guid.NewGuid();
            CashAdvanceId = cashAdvanceId;
            Amount = amount;
            Date = DateTime.UtcNow;
            BankAccount = bankAccount;
        }
    }
}
