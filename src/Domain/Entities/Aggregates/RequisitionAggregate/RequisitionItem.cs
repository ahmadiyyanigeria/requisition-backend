namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class RequisitionItem
    {
        private readonly decimal _totalPrice;

        public RequisitionItem(Guid requisitionId, string description, int quantity, decimal unitPrice)
        {
            RequisitionItemId = Guid.NewGuid();
            RequisitionId = requisitionId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            _totalPrice = quantity * unitPrice; // Set the backing field
        }

        public Guid RequisitionItemId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => _totalPrice;
    }
}
