using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.Common;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class Requisition
    {
        public Guid RequisitionId { get; private set; } = Guid.NewGuid();
        public Guid SubmitterId { get; private set; } = default!; 
        public string Description { get; private set; } = default!;
        public string ExpenseHead { get; private set; } = default!;
        public RequisitionStatus Status { get; private set; } = RequisitionStatus.Draft;
        public DateTime RequestedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ApprovedDate { get; private set; }
        public DateTime? RejectedDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public ApprovalFlow ApprovalFlow { get; private set; } = default!;
        public Guid? ExpenseAccountId { get; private set; }
        public RequisitionType RequisitionType { get; private set; }
        public BankAccount? BankAccount { get; private set; }
        public string Department { get; private set; } = default!;
        public Submitter Submitter { get; private set; } = default!;

        private readonly List<RequisitionItem> _items = [];
        private readonly List<Attachment> _attachments = [];

        public IReadOnlyList<Attachment> Attachments => _attachments.AsReadOnly();
        public IReadOnlyList<RequisitionItem> Items => _items.AsReadOnly();

        private Requisition() { }

        public Requisition(Guid submitterId, string description, string expenseHead, RequisitionType requisitionType, BankAccount? bankAccount, string department)
        {
            SubmitterId = submitterId;
            Description = description;
            ExpenseHead = expenseHead;
            RequisitionType = requisitionType;
            BankAccount = bankAccount;
            Department = department ?? "Default Department";           
        }

        public void AddItem(RequisitionItem item)
        {
            if (item == null)
            {
                throw new DomainException("Requisition item cannot be null", ExceptionCodes.NullRequisitionItem.ToString(), 400);
            }
            _items.Add(item);
            CalculateTotalAmount();
        }

        public void RemoveItem(RequisitionItem item)
        {
            if (item == null)
            {
                throw new DomainException("Requisition item cannot be null", ExceptionCodes.NullRequisitionItem.ToString(), 400);
            }
            _items.Remove(item);
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = _items.Sum(item => item.TotalPrice);
        }

        public void SetApprovalFlow(ApprovalFlow approvalFlow)
        {
            ApprovalFlow = approvalFlow ?? throw new DomainException($"Approval flow not configured", ExceptionCodes.NullApprovalFlow.ToString(), 500);
        }

        public void SetStatus(RequisitionStatus status)
        {
            Status = status;
        }

        public void SetRequisitionPending()
        {
            Status = RequisitionStatus.Pending;
        }

        public void ApproveCurrentStep(string approverId, string notes)
        {
            if (Status == RequisitionStatus.Pending || Status == RequisitionStatus.InProgress)
            {
                var currentApprover = ApprovalFlow.GetCurrentApprover();
                if (currentApprover.ApproverId == approverId)
                {
                    currentApprover.Approve(notes);
                    if (ApprovalFlow.IsFinalStep())
                    {
                        Status = RequisitionStatus.Approved;
                        ApprovedDate = DateTime.UtcNow;
                    }
                    else
                    {
                        ApprovalFlow.MoveToNextStep();
                    }
                }
                else
                {
                    throw new DomainException($"You are not authorized to approve this step.", ExceptionCodes.Unauthorized.ToString(), 403);
                }
            }
            else
            {
                throw new DomainException($"Requisition is not in pending state.", ExceptionCodes.InvalidApprovalState.ToString(), 400);
            }
        }

        public void RejectCurrentStep(string approverId, string notes)
        {
            if (Status == RequisitionStatus.Pending)
            {
                var currentApprover = ApprovalFlow.GetCurrentApprover();
                if (currentApprover.ApproverId == approverId)
                {
                    currentApprover.Reject(notes);
                    Status = RequisitionStatus.Rejected;
                    RejectedDate = DateTime.UtcNow;
                }
                else
                {
                    throw new DomainException($"You are not authorized to approve this step.", ExceptionCodes.Unauthorized.ToString(), 403);
                }
            }
            else
            {
                throw new DomainException($"Requisition is not in pending state.", ExceptionCodes.InvalidApprovalState.ToString(), 400);
            }
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

            // Example: Generate order, notify stakeholders, etc.

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