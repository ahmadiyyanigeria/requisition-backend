namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class PurchaseOrderItem
    {
        public Guid PurchaseOrderItemId { get; private set; }
        public Guid PurchaseOrderId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        private PurchaseOrderItem() { }
        public PurchaseOrderItem(string description, int quantity, decimal unitPrice, Guid purchaseOrderId)
        {
            PurchaseOrderItemId = Guid.NewGuid();
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            PurchaseOrderId = purchaseOrderId;
        }
    }
}
