﻿using Domain.Enums;

namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class Payment
    {
        public Guid PaymentId { get; private set; }
        public Guid PurchaseOrderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public string ReferenceNumber { get; private set; } = default!;

        private Payment() { }
        public Payment(decimal amount, PaymentMethod paymentMethod, string referenceNumber, Guid purchaseOrderId)
        {
            PaymentId = Guid.NewGuid();
            Amount = amount;
            PaymentDate = DateTime.UtcNow;
            PaymentMethod = paymentMethod;
            ReferenceNumber = referenceNumber;
            PurchaseOrderId = purchaseOrderId;
        }
    }
}
