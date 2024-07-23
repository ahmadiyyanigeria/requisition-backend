namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class RequisitionItem(Guid requisitionId, string description, int quantity, decimal unitPrice)
    {
        private readonly decimal _totalPrice = quantity * unitPrice;

        public Guid RequisitionItemId { get; private set; } = Guid.NewGuid();
        public Guid RequisitionId { get; private set; } = requisitionId;
        public string Description { get; private set; } = description;
        public int Quantity { get; private set; } = quantity;
        public decimal UnitPrice { get; private set; } = unitPrice;
        public decimal TotalPrice => _totalPrice;
    }
}
