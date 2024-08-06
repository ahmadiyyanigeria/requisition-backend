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
        public DateTime? LastDateModified { get; private set; }
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
                throw new DomainException("Requisition item cannot be null");
            }
            _items.Add(item);
            CalculateTotalAmount();
        }

        public void RemoveItem(RequisitionItem item)
        {
            if (item == null)
            {
                throw new DomainException("Requisition item cannot be null");
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
            ApprovalFlow = approvalFlow ?? throw new DomainException($"Approval flow not configured");
        }

        public void SetRequisitionPending()
        {
            Status = RequisitionStatus.Pending;
        }

        public void SetRequisitionClosed()
        {
            Status = RequisitionStatus.Closed;
        }

        public void SetRequisitionProcessed(RequisitionType type)
        {
            if (IsAlreadyProcessed(Status))
            {
                throw new DomainException("Requisition already processed.");
            }

            if (Status == RequisitionStatus.Closed)
            {
                throw new DomainException("Requisition already closed.");
            }

            if (Status != RequisitionStatus.Approved)
            {
                throw new DomainException("Requisition must be approved before processing.");
            }

            Status = type switch
            {
                RequisitionType.CashAdvance => RequisitionStatus.CAGenerated,
                RequisitionType.Grant => RequisitionStatus.GrantGenerated,
                RequisitionType.PurchaseOrder => RequisitionStatus.POGenerated,
                _ => throw new DomainException("Invalid requisition type.", nameof(type))
            };

            LastDateModified = DateTime.UtcNow;
        }

        private bool IsAlreadyProcessed(RequisitionStatus status)
        {
            return status == RequisitionStatus.CAGenerated ||
                   status == RequisitionStatus.POGenerated ||
                   status == RequisitionStatus.GrantGenerated;
        }

        public void ApproveCurrentStep(string approverId, string? notes)
        {
            ValidateCurrentState();

            //var currentApprover = GetCurrentApprover(approverId);
            var approver = GetApprover(approverId);

            if (ApprovalFlow.CanApprove(approverId))
            {
                // Approve if the approver has the same or higher order
                approver.Approve(notes);

                if (ApprovalFlow.IsFinalStep(approverId))
                {
                    Status = RequisitionStatus.Approved;
                    ApprovedDate = DateTime.UtcNow;
                }
                else
                {
                    ApprovalFlow.MoveToNextStep(approverId);
                }
            }
            else
            {
                throw new DomainException("You are not authorized to approve or reject this step.");
            }

            LastDateModified = DateTime.UtcNow;
        }

        public void RejectCurrentStep(string approverId, string notes)
        {
            ValidateCurrentState();

            var currentApprover = GetCurrentApprover(approverId);

            if (ApprovalFlow.CanApprove(approverId))
            {
                if (string.IsNullOrEmpty(notes))
                {
                    throw new DomainException("Notes cannot be null or empty when rejecting a requisition.");
                }

                currentApprover.Reject(notes);
                Status = RequisitionStatus.Rejected;
                RejectedDate = DateTime.UtcNow;
            }
            else
            {
                throw new DomainException("You are not authorized to approve or reject this step.");
            }

            LastDateModified = DateTime.UtcNow;
        }

        private void ValidateCurrentState()
        {
            if (Status == RequisitionStatus.Approved )
            {
                throw new DomainException("Requisition is already approved.");
            }

            if (Status != RequisitionStatus.Pending && Status != RequisitionStatus.InApproval)
            {
                throw new DomainException("Requisition is not in a pending or approval state.");
            }
        }

        private ApprovalStep GetCurrentApprover(string approverId)
        {
            var currentApprover = ApprovalFlow.GetCurrentApprover();
            if (currentApprover.ApproverId != approverId)
            {
                throw new DomainException("You are not authorized to approve or reject this step.");
            }
            return currentApprover;
        }

        private ApprovalStep GetApprover(string approverId)
        {
            var currentApprover = ApprovalFlow.GetApprover(approverId);
            return currentApprover is null
                ? throw new DomainException("You are not authorized to approve or reject this step.")
                : currentApprover;
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
    }
}