namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class PurchaseOrderItem(string description, int quantity, decimal unitPrice, Guid purchaseOrderId)
    {
        private readonly decimal _totalPrice = quantity * unitPrice;
        public Guid PurchaseOrderItemId { get; private set; } = Guid.NewGuid();
        public Guid PurchaseOrderId { get; private set; } = purchaseOrderId;
        public string Description { get; private set; } = description;
        public int Quantity { get; private set; } = quantity;
        public decimal UnitPrice { get; private set; } = unitPrice;
        public decimal TotalPrice => _totalPrice;
    }
}
