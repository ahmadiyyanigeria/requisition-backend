namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class PurchaseOrderItem
    {
        public Guid ItemId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public PurchaseOrderItem(string description, int quantity, decimal unitPrice)
        {
            ItemId = Guid.NewGuid();
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
