using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class ReimbursementEntry
    {
        public Guid ReimbursementEntryId { get; private set; }
        public Guid CashAdvanceId { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Attachment Receipt { get; private set; }

        public ReimbursementEntry(Guid cashAdvanceId, string description, decimal amount, Attachment receipt)
        {
            ReimbursementEntryId = Guid.NewGuid();
            CashAdvanceId = cashAdvanceId;
            Description = description;
            Amount = amount;
            Date = DateTime.UtcNow;
            Receipt = receipt;
        }
    }
}
