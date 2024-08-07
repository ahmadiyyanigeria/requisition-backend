﻿using Domain.Entities.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class PurchaseOrder
    {
        public Guid PurchaseOrderId { get; private set; }
        public Guid RequisitionId { get; private set; }
        public Guid ProcessorId { get; private set; }
        public string Notes { get; private set; } = default!;
        public Guid VendorId { get; private set; }
        public Vendor Vendor { get; private set; } = default!;
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public PurchaseOrderStatus Status { get; private set; } = PurchaseOrderStatus.Requested;
        //public Guid AttachmentId { get; private set; }
        //public Attachment Invoice { get; private set; } = default!;

        private readonly List<PurchaseOrderItem> _items = [];
        private readonly List<Payment> _payments = [];
        public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();
        public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();

        private PurchaseOrder() { }
        public PurchaseOrder(Guid requisitionId, Guid vendorId, Guid processorId, string note)
        {
            PurchaseOrderId = Guid.NewGuid();
            RequisitionId = requisitionId;
            VendorId = vendorId;
            ProcessorId = processorId;
            Notes = note;
        }

        public void Fulfill()
        {
            if (Status == PurchaseOrderStatus.Requested)
            {
                Status = PurchaseOrderStatus.Fulfilled;
                DeliveryDate = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException($"Purchase order is not in requested state.");
            }
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
