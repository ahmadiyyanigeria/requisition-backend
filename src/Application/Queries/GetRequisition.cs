using Application.Repositories;
using Domain.Enums;
using Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Exceptions;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Queries
{
    public class GetRequisition
    {
        public record Query : IRequest<RequisitionResponse>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, RequisitionResponse>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IRequisitionRepository requisitionRepository, ILogger<Handler> logger)
            {
                _requisitionRepository = requisitionRepository;
                _logger = logger;
            }
            public async Task<RequisitionResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var requisition = await _requisitionRepository.GetByIdAsync(request.Id);
                if (requisition is null)
                {
                    _logger.LogError("Requisition with Id {Id} does not exist", request.Id);
                    throw new ApplicationException($"Requisition with Id {request.Id} does not exists", ExceptionCodes.RequisitionNotFound.ToString(), 404);
                }

                var approvalList = requisition.ApprovalFlow.ApproverSteps
                .Select(step => new Approval(step.Role, step.Status.ToString())).ToList();

                var submitterName = requisition.Submitter.Name;
                var items = requisition.Items.Select(a => new Item(a.Description, a.UnitPrice, a.Quantity, a.TotalPrice)).ToList();

                var requisitionResponse = new RequisitionResponse(requisition.RequisitionId, submitterName, requisition.Description, requisition.ExpenseHead, requisition.Status, requisition.RequestedDate, requisition.ApprovedDate, requisition.RejectedDate, requisition.LastDateModified, requisition.TotalAmount, approvalList, requisition.RequisitionType, requisition.Department, items, requisition.Attachments);

                return requisitionResponse;
            }
        }
        public record RequisitionResponse(Guid RequisitionId, string SubmitterName, string Description, string ExpenseHead, RequisitionStatus Status, DateTime RequestedDate, DateTime? ApprovedDate, DateTime? RejectedDate, DateTime? LastDateModified, decimal TotalAmount, IReadOnlyList<Approval> ApprovalList,RequisitionType RequisitionType, string Department,IReadOnlyList<Item> Items, IReadOnlyList<Attachment> Attachments);

        public record Approval(string Role, string Status);

        public record Item(string Description, decimal UnitPrice, int Quantity, decimal TotalPrice);
    }
}
