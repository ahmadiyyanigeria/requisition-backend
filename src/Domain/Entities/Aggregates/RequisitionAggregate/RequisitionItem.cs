using Domain.Exceptions;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class RequisitionItem
    {
        private readonly decimal _totalPrice;

        public Guid RequisitionItemId { get; private set; } = Guid.NewGuid();
        public Guid RequisitionId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => _totalPrice;

        public RequisitionItem(Guid requisitionId, string description, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Item quantity must be greater than zero.");
            }

            if (unitPrice <= 0)
            {
                throw new DomainException("Item unit price must be greater than zero.");
            }

            RequisitionId = requisitionId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            _totalPrice = quantity * unitPrice;
        }
    }
}
