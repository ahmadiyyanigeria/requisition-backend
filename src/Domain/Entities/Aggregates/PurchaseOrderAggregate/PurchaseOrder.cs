using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;

namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class PurchaseOrder
    {
        public Guid PurchaseOrderId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid VendorId { get; private set; }
        public Vendor Vendor { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public PurchaseOrderStatus Status { get; private set; }
        public Attachment Invoice { get; private set; }

        private readonly List<PurchaseOrderItem> _items = [];
        private readonly List<Payment> _payments = [];
        public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();
        public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();

        public PurchaseOrder(Guid requisitionId, Guid vendorId, Vendor vendor)
        {
            PurchaseOrderId = Guid.NewGuid();
            RequisitionId = requisitionId;
            VendorId = vendorId;
            Vendor = vendor;
            OrderDate = DateTime.UtcNow;
            Status = PurchaseOrderStatus.Pending;
        }

        public void Approve()
        {
            if (Status == PurchaseOrderStatus.Pending)
            {
                Status = PurchaseOrderStatus.Approved;
            }
            // Consider throwing an exception or handling other cases
        }

        public void Reject()
        {
            if (Status == PurchaseOrderStatus.Pending)
            {
                Status = PurchaseOrderStatus.Rejected;
            }
            // Consider throwing an exception or handling other cases
        }

        public void Fulfill()
        {
            if (Status == PurchaseOrderStatus.Approved)
            {
                Status = PurchaseOrderStatus.Fulfilled;
            }
            // Consider throwing an exception or handling other cases
        }

        public void AddItem(PurchaseOrderItem item)
        {
            _items.Add(item);
            CalculateTotalAmount();
        }

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
            if (_payments.Sum(p => p.Amount) >= TotalAmount)
            {
                Status = PurchaseOrderStatus.Paid;
            }
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = _items.Sum(item => item.TotalPrice);
        }
    }
}
