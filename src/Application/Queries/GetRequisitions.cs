using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Enums;
using Domain.Paging;
using Domain.Repositories;
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
            public string? Department { get; set; }
        }

        public record RequisitionResponse(Guid RequisitionId, string RequisitionNumber, string SubmitterName, string ExpenseHeadName, RequisitionStatus Status, DateTime RequestedDate, decimal TotalAmount, RequisitionType RequisitionType, string Department);


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
