using Domain.Entities.Common;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class RetirementEntry
    {
        public Guid RetirementEntryId { get; private set; }
        public Guid CashAdvanceId { get; private set; }
        public string Description { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        //public Guid AttachmentId { get; private set; }
        //public Attachment Receipt { get; private set; } = default!;
        public string Type => "Expense";

        private RetirementEntry() { }
        public RetirementEntry(Guid cashAdvanceId, string description, decimal amount, Guid receipt)
        {
            RetirementEntryId = Guid.NewGuid();
            CashAdvanceId = cashAdvanceId;
            Description = description;
            Amount = amount;
            Date = DateTime.UtcNow;
            //AttachmentId = receipt;
        }
    }
}
