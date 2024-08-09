using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.Aggregates.CashAdvanceAggregate
{
    public class ReimbursementEntry
    {
        public Guid ReimbursementEntryId { get; private set; } = Guid.NewGuid();
        public Guid CashAdvanceId { get; private set; }
        public string Description { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public ReimbursementStatus Status { get; private set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow;
        //public Guid AttachmentId { get; private set; }
        //public Attachment Receipt { get; private set; } = default!;

        private ReimbursementEntry() { }

        public ReimbursementEntry(Guid cashAdvanceId, string description, decimal amount)
        {
            CashAdvanceId = cashAdvanceId;
            Description = description;
            Amount = amount;
            //AttachmentId = receiptId;
        }

        public void SetReimbursementPaid()
        {
            Status = ReimbursementStatus.Paid;
        }
    }
}
