using Application.Common.Interfaces;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;
using Domain.Enums;
using FluentValidation;
using MediatR;
using static Application.Commands.CreateRequisition;

namespace Application.Commands
{
    public class CreateRequisition
    {
        public class CreateRequisitionCommand : IRequest<Guid>
        {
            public string Description { get; init; } = default!; 
            public Guid ExpenseAccountId { get; }
            public RequisitionType RequisitionType { get; }
            public string AccountNumber { get; init; } = default!;
            public string Department { get; init; } = default!;
            public List<RequisitionItemDto> Items { get; set; }
            public List<AttachmentDto> Attachments { get; set; }
            public bool IsDraft { get; set; }  // Indicates if the submission is a draft
        }

        public class RequisitionItemDto
        {
            public string Description { get; init; } = default!;
            public int Quantity { get; init; }
            public decimal UnitPrice { get; init; }
        }

        public class AttachmentDto
        {
            public required string FileName { get; init; } = default!;
            public required string FileType { get; init; } = default!;
            public required string FileUrl { get; init; }
        }

        public class Handler : IRequestHandler<CreateRequisitionCommand, Guid>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ISubmitterRepository _submitterRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IApprovalFlowService _approvalFlowService;
            private readonly ICurrentUser _user;
            public Handler(IRequisitionRepository requisitionRepository, ISubmitterRepository submitterRepository, IUnitOfWork unitOfWork, IApprovalFlowService approvalFlowService, ICurrentUser user)
            {
                _requisitionRepository = requisitionRepository;
                _submitterRepository = submitterRepository;
                _unitOfWork = unitOfWork;
                _approvalFlowService = approvalFlowService;
                _user = user;
            }

            public async Task<Guid> Handle(CreateRequisitionCommand request, CancellationToken cancellationToken)
            {
                //get current logged in user
                var currentUserEmail = _user.GetUserEmail();

                //use the current logged in user email to get the submitter details
                var submitter = await _submitterRepository.GetByEmailAsync(currentUserEmail!);
                var submitterRole = submitter!.Position;

                var requisition = new Requisition(
                    submitter.SubmitterId,
                    request.Description,
                    request.ExpenseAccountId,
                    request.RequisitionType,
                    request.AccountNumber,
                    request.Department);

                foreach (var item in request.Items)
                {
                    var requisitionItem = new RequisitionItem(requisition.RequisitionId, item.Description, item.Quantity, item.UnitPrice);
                    requisition.AddItem(requisitionItem);
                }

                foreach (var attachment in request.Attachments)
                {
                    var attachmentEntity = new Attachment(attachment.FileName, attachment.FileType, attachment.FileUrl);
                    requisition.AddAttachment(attachmentEntity);
                }

                if (!request.IsDraft)
                {
                    requisition.SetStatus(RequisitionStatus.Pending);
                }

                //create approval flow for the requisition
                var approvalFlow = _approvalFlowService.CreateApprovalFlow(requisition, submitterRole);
                //set the flow for the requisition
                requisition.SetApprovalFlow(approvalFlow);

                await _requisitionRepository.AddAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return requisition.RequisitionId;
            }
        }
    }

    public class CommandValidator : AbstractValidator<CreateRequisitionCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("Description contains invalid characters.");

            RuleFor(x => x.ExpenseAccountId)
                .NotEmpty().WithMessage("Expense Account ID is required.");

            RuleFor(x => x.RequisitionType)
                .IsInEnum().WithMessage("Invalid requisition type.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account Number is required.")
                .Matches(@"^\d+$").WithMessage("Account Number must be numeric.");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required.")
                .Must(ScriptContentValidator.NotContainScript).WithMessage("Department contains invalid characters.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Requisition must contain at least one item.")
                .Must(items => items.All(item => item.Quantity > 0)).WithMessage("All items must have a quantity greater than zero.")
                .Must(items => items.All(item => item.UnitPrice > 0)).WithMessage("All items must have a unit price greater than zero.");

            RuleForEach(x => x.Items).SetValidator(new RequisitionItemDtoValidator());

            RuleForEach(x => x.Attachments).SetValidator(new AttachmentDtoValidator());
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
                .Must(ScriptContentValidator.NotContainScript).WithMessage("File type contains invalid characters.");

            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("Attachment url is required.");
        }
    }
}
