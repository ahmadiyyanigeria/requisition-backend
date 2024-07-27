using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Entities.Common;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

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

            public Handler(IRequisitionRepository requisistionRepository, ILogger<Handler> logger)
            {
                _requisitionRepository = requisistionRepository;
                _logger = logger;
            }
            public async Task<RequisitionResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var requisition = await _requisitionRepository.GetByIdAsync(request.Id);
                if (requisition is null)
                {
                    _logger.LogError("Requisition with Id {Id} does not exist", request.Id);
                    throw new DomainException($"Requisition with Id {request.Id} does not exists", ExceptionCodes.RequisitionNotFound.ToString(), 404);
                }
                return requisition.Adapt<RequisitionResponse>();
            }
        }
        public record RequisitionResponse(Guid RequisitionId, Guid SubmitterId, string Description, string ExpenseHead, RequisitionStatus Status, DateTime RequestedDate, DateTime? ApprovedDate, DateTime? RejectedDate, DateTime? LastDateModified, decimal TotalAmount, ApprovalFlow ApprovalFlow, Guid? ExpenseAccountId, RequisitionType RequisitionType, string Department,List<RequisitionItem> Items, List<Attachment> Attachments);
    }
}
