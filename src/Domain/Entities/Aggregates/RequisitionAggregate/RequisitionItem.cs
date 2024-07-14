namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class RequisitionItem(string description, int quantity, decimal unitPrice)
    {
        public Guid RequisitionItemId { get; private set; } = Guid.NewGuid();
        public string Description { get; private set; } = description;
        public int Quantity { get; private set; } = quantity;
        public decimal UnitPrice { get; private set; } = unitPrice;
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
