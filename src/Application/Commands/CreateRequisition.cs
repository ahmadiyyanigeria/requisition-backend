using Application.Common.Interfaces;
using Application.Helpers;
using Application.Services;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.Common;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Factories;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using static Application.Commands.CreateRequisition;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public class CreateRequisition
    {
        public record CreateRequisitionCommand(string Description, string ExpenseHead, RequisitionType RequisitionType, BankAccountDto? BankAccount, IReadOnlyList<RequisitionItemDto> Items, IReadOnlyList<AttachmentDto>? Attachments, bool IsDraft) : IRequest<RequisitionResponse>;

        public record RequisitionItemDto(string Description, int Quantity, decimal UnitPrice);
        public record AttachmentDto(string FileName, string FileType, string FileUrl);
        public record BankAccountDto(string AccountNumber, string BankName, string AccountName, string? IBAN, string? SWIFT);

        public class Handler : IRequestHandler<CreateRequisitionCommand, RequisitionResponse>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ISubmitterRepository _submitterRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IApprovalFlowService _approvalFlowService;
            private readonly ICurrentUser _user;
            private readonly IRequisitionFactory _requisitionFactory;
            private readonly IExpenseHeadRepository _expenseHeadRepository;
            public Handler(IRequisitionRepository requisitionRepository, ISubmitterRepository submitterRepository, IUnitOfWork unitOfWork, IApprovalFlowService approvalFlowService, ICurrentUser user, IRequisitionFactory requisitionFactory, IExpenseHeadRepository expenseHeadRepository)
            {
                _requisitionRepository = requisitionRepository;
                _submitterRepository = submitterRepository;
                _unitOfWork = unitOfWork;
                _approvalFlowService = approvalFlowService;
                _user = user;
                _requisitionFactory = requisitionFactory;
                _expenseHeadRepository = expenseHeadRepository;
            }

            public async Task<RequisitionResponse> Handle(CreateRequisitionCommand request, CancellationToken cancellationToken)
            {
                //get current logged in user //userId, userrole, name, department, email, phonenumber
                var user = _user.GetUserDetails();
                string department = "HR";

                //create submitter record
                var submitter = new Submitter(user.UserId, user.Name, user.Email, user.Role, department);

                //validate expense head
                var expensehead = await _expenseHeadRepository.GetByNameAsync(request.ExpenseHead);
                if (expensehead is null)
                {
                    throw new ApplicationException($"Invalid expense head.", ExceptionCodes.InvalidExpenseHead.ToString(), 400);
                }

                // Validate BankAccount if required
                BankAccount? bankData = null;
                if (request.RequisitionType is RequisitionType.CashAdvance or RequisitionType.Grant)
                {
                    if (request.BankAccount is null)
                    {
                        throw new ApplicationException($"Bank account details not provided.", ExceptionCodes.BankDetailsNotProvided.ToString(), 400);
                    }
                    bankData = new BankAccount(request.BankAccount.AccountNumber, request.BankAccount.BankName, request.BankAccount.AccountName, request.BankAccount.IBAN, request.BankAccount.SWIFT);
                }

                //creating the requisition object
                var requisition = await _requisitionFactory.Create(submitter.SubmitterId, request.Description,
                    request.ExpenseHead, request.RequisitionType, bankData, department);

                foreach (var item in request.Items)
                {
                    var requisitionItem = new RequisitionItem(requisition.RequisitionId, item.Description, item.Quantity, item.UnitPrice);
                    requisition.AddItem(requisitionItem);
                }

                if(request.Attachments != null)
                {
                    foreach (var attachment in request.Attachments)
                    {
                        var attachmentEntity = new Attachment(attachment.FileName, attachment.FileType, attachment.FileUrl);
                        requisition.AddAttachment(attachmentEntity);
                    }
                }

                if (!request.IsDraft)
                {
                    requisition.SetRequisitionPending();
                }

                //create approval flow for the requisition
                var approvalFlow = _approvalFlowService.CreateApprovalFlow(requisition, user.Role);

                //set the flow for the requisition
                requisition.SetApprovalFlow(approvalFlow);

                await _submitterRepository.AddAsync(submitter);
                await _requisitionRepository.AddAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var approvalList = requisition.ApprovalFlow.ApproverSteps
                .Select(step => new Approval(step.Role, step.Status.ToString(), step.Notes)).ToList();

                var items = requisition.Items.Select(a => new Item(a.Description, a.UnitPrice, a.Quantity, a.TotalPrice)).ToList();

                var requisitionResponse = new RequisitionResponse(requisition.RequisitionId, requisition.RequisitionNumber, submitter.Name, requisition.Description, requisition.ExpenseHeadName, requisition.Status, requisition.RequestedDate, requisition.ApprovedDate, requisition.RejectedDate, requisition.LastDateModified, requisition.TotalAmount, approvalList, requisition.RequisitionType, requisition.Department, items, requisition.Attachments);

                return requisitionResponse;
            }
        }
    }

    public record RequisitionResponse(Guid RequisitionId, string RequisitionNumber, string SubmitterName, string Description, string ExpenseHead, RequisitionStatus Status, DateTime RequestedDate, DateTime? ApprovedDate, DateTime? RejectedDate, DateTime? LastDateModified, decimal TotalAmount, IReadOnlyList<Approval> ApprovalList, RequisitionType RequisitionType, string Department, IReadOnlyList<Item> Items, IReadOnlyList<Attachment> Attachments);

    public record Approval(string Role, string Status, string? Comment);

    public record Item(string Description, decimal UnitPrice, int Quantity, decimal TotalPrice);

    public class CommandValidator : AbstractValidator<CreateRequisitionCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("Description contains invalid characters.");

            RuleFor(x => x.ExpenseHead)
                .NotEmpty().WithMessage("Expense head is required.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("Expense head contains invalid characters.");

            RuleFor(x => x.RequisitionType)
                .IsInEnum().WithMessage("Invalid requisition type.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Requisition must contain at least one item.")
                .Must(items => items.All(item => item.Quantity > 0)).WithMessage("All items must have a quantity greater than zero.")
                .Must(items => items.All(item => item.UnitPrice > 0)).WithMessage("All items must have a unit price greater than zero.");

            RuleForEach(x => x.Items).SetValidator(new RequisitionItemDtoValidator());

            When(x => x.Attachments != null && x.Attachments.Any(), () =>
            {
                RuleForEach(x => x.Attachments).SetValidator(new AttachmentDtoValidator());
            });

            When(x => x.BankAccount != null, () =>
            {
                RuleFor(x => x.BankAccount)
                    .SetValidator(new BankAccountValidator());
            });
        }
    }

    public class RequisitionItemDtoValidator : AbstractValidator<RequisitionItemDto>
    {
        public RequisitionItemDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Item description is required.")
                .MaximumLength(200).WithMessage("Item description cannot exceed 200 characters.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("Item description contains invalid characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }

    public class AttachmentDtoValidator : AbstractValidator<AttachmentDto>
    {
        public AttachmentDtoValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File name is required.")
                .MaximumLength(255).WithMessage("File name cannot exceed 255 characters.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("File name contains invalid characters.");

            RuleFor(x => x.FileType)
                .NotEmpty().WithMessage("File type is required.")
                .Must(fileType => AllowedFileTypes.Contains(fileType)).WithMessage("Invalid file type.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("File type contains invalid characters.");

            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("Attachment URL is required.");
        }

        private static readonly HashSet<string> AllowedFileTypes = new HashSet<string>
        {
            "image/jpeg",
            "image/png",
            "application/pdf",
            "application/vnd.ms-excel",
            // Add more allowed file types as necessary
        };
    }

    public class BankAccountValidator : AbstractValidator<BankAccountDto>
    {
        public BankAccountValidator()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Bank account number is required.")
                .Length(10).WithMessage("Bank account number must be 10 digits.");

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required.")
                .MaximumLength(100).WithMessage("Bank name cannot exceed 100 characters.");
        }
    }

}
