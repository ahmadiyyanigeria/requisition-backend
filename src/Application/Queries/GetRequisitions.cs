using Application.Common.Interfaces;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Entities.Common;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetRequisitions
    {
        public record Query(bool UsePaging = true, DateTime ? RequestedStartDate = null,
            DateTime? RequestedEndDate = null,
            RequisitionStatus? Status = null,
            decimal? MinTotalAmount = null,
            decimal? MaxTotalAmount = null,
            string? ExpenseHead = null,
            RequisitionType? RequisitionType = null) : PageRequest, IRequest<PaginatedList<RequisitionResponse>>
        {
            public string? Department { get; set; } = null;
        }

        public record RequisitionResponse
        {
            public Guid RequisitionId { get; set; }
            public string SubmitterName { get; set; } = default!;
            public string ExpenseHead { get; set; } = default!;
            public RequisitionStatus Status { get; set; }
            public DateTime RequestedDate { get; set; }
            public decimal TotalAmount { get; set; }
            public RequisitionType RequisitionType { get; set; }
            public string Department { get; set; } = default!;
        }

        public class Handler : IRequestHandler<Query, PaginatedList<RequisitionResponse>>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ISubmitterRepository _submitterRepository;
            private readonly ICurrentUser _user;
            public Handler(IRequisitionRepository requisitionRepository, ICurrentUser user, ISubmitterRepository submitterRepository)
            {
                _requisitionRepository = requisitionRepository;
                _user = user;
                _submitterRepository = submitterRepository;
            }
            public async Task<PaginatedList<RequisitionResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = _user.GetUserDetails();
                IReadOnlyList<Guid>? submitterId = null;
                if(user.Role == Roles.Employee)
                {
                    var submitter = await _submitterRepository.GetByUserIdAsync(user.UserId);
                    if(submitter == null)
                    {
                        return new PaginatedList<RequisitionResponse>();
                    }
                    submitterId = submitter.Select(x => x!.SubmitterId).ToList();
                }

                if(user.Role == Roles.HOD)
                {
                    request.Department = "HR";
                }

                var requisitions = await _requisitionRepository.GetRequisitions(request,
                request.UsePaging, request.RequestedStartDate, request.RequestedEndDate, request.Status, request.MinTotalAmount,request.MaxTotalAmount, submitterId, request.ExpenseHead, request.RequisitionType, request.Department);
                return requisitions.Adapt<PaginatedList<RequisitionResponse>>();
            }
        }
    }
}
