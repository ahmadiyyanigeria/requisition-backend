using Domain.Entities.Aggregates.RequisitionAggregate;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class RetirementEntry
    {
        public Guid RetirementEntryId { get; private set; }
        public Guid CashAdvanceId { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Attachment Receipt { get; private set; }
        public string Type => "Expense";

        public RetirementEntry(Guid cashAdvanceId, string description, decimal amount, Attachment receipt)
        {
            RetirementEntryId = Guid.NewGuid();
            CashAdvanceId = cashAdvanceId;
            Description = description;
            Amount = amount;
            Date = DateTime.UtcNow;
            Receipt = receipt;
        }
    }
}
