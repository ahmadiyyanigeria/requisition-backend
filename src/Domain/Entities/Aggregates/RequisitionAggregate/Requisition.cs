using Domain.Entities.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class Requisition
    {
        public Guid RequisitionId { get; private set; } = Guid.NewGuid();
        public Guid SubmitterId { get; private set; }
        public string Description { get; private set; }
        public RequisitionStatus Status { get; private set; } = RequisitionStatus.Pending;
        public DateTime RequestedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ApprovedDate { get; private set; }
        public DateTime? RejectedDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public ApprovalFlow ApprovalFlow { get; private set; }
        public Guid ExpenseAccountId { get; private set; }
        public RequisitionType RequisitionType { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public string Department { get; private set; }

        private readonly List<RequisitionItem> _items = [];
        private readonly List<Attachment> _attachments = [];

        public IReadOnlyList<Attachment> Attachments => _attachments.AsReadOnly();
        public IReadOnlyList<RequisitionItem> Items => _items.AsReadOnly();

        public Requisition(Guid submitterId, string description, Guid expenseAccountId, RequisitionType requisitionType, BankAccount bankAccount, string department)
        {
            SubmitterId = submitterId;
            Description = description;
            ExpenseAccountId = expenseAccountId;
            RequisitionType = requisitionType;
            BankAccount = bankAccount;
            Department = department ?? "Default Department"; // Assuming you have a method to get the default department of the submitter
        }

        public void Approve(Guid approverId, string notes)
        {
            if (Status == RequisitionStatus.Pending)
            {
                Status = RequisitionStatus.Approved;
                ApprovedDate = DateTime.UtcNow;
                // Additional logic for approval
            }
            // Consider throwing an exception or handling other cases
        }

        public void Reject(Guid approverId, string notes)
        {
            if (Status == RequisitionStatus.Pending)
            {
                Status = RequisitionStatus.Rejected;
                RejectedDate = DateTime.UtcNow;
                // Additional logic for rejection
            }
            // Consider throwing an exception or handling other cases
        }

        public void AddItem(RequisitionItem item)
        {
            _items.Add(item);
            CalculateTotalAmount();
        }

        public void RemoveItem(RequisitionItem item)
        {
            _items.Remove(item);
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = _items.Sum(item => item.TotalPrice);
        }

        public void AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
        }

        public void RemoveAttachment(Guid attachmentId)
        {
            var attachment = _attachments.FirstOrDefault(a => a.AttachmentId == attachmentId);
            if (attachment != null)
            {
                _attachments.Remove(attachment);
            }
        }

        public void ProcessAsPurchaseOrder()
        {
            if (RequisitionType != RequisitionType.PurchaseOrder)
            {
                throw new InvalidOperationException("Requisition type is not PurchaseOrder.");
            }

            ValidateApproval();

            // Example: Generate order,notify stakeholders, etc.

            Status = RequisitionStatus.Processed;
            // Set additional properties or perform other actions as needed
        }

        public void ProcessAsCashAdvance()
        {
            if (RequisitionType != RequisitionType.CashAdvance)
            {
                throw new InvalidOperationException("Requisition type is not CashAdvance.");
            }

            ValidateApproval();

            // Example: Disburse funds, record transactions, manage repayments, etc.

            Status = RequisitionStatus.Processed;
            // Set additional properties or perform other actions as needed
        }

        public void ProcessAsGrant()
        {
            if (RequisitionType != RequisitionType.Grant)
            {
                throw new InvalidOperationException("Requisition type is not Grant.");
            }

            ValidateApproval();

            // Example: Allocate funds, manage reporting requirements, etc.

            Status = RequisitionStatus.Processed;
            // Set additional properties or perform other actions as needed
        }

        private void ValidateApproval()
        {
            if (Status != RequisitionStatus.Approved)
            {
                throw new InvalidOperationException("Requisition must be approved before processing.");
            }
        }
    }
}