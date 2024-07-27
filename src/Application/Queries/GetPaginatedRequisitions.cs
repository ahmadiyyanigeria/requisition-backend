using Application.Paging;
using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetPaginatedRequisitions
    {
        public record Query : PageRequest, IRequest<PaginatedList<RequisitionResponse>>
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? ExpenseHead { get; set; }
            public string? Department { get; set; }
            public string? StatusFilter { get; set; }
            public string? TypeFilter { get; set; }
            public HashSet<RequisitionStatus> RequisitionStatuses => string.IsNullOrWhiteSpace(StatusFilter) ? new HashSet<RequisitionStatus>() : StatusFilter.Split(',')
            .Select(s => Enum.TryParse<RequisitionStatus>(s, out var requisitionStatus) ? requisitionStatus : (RequisitionStatus?)null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToHashSet();          
            public HashSet<RequisitionType> RequisitionTypes => string.IsNullOrWhiteSpace(TypeFilter) ? new HashSet<RequisitionType>() : TypeFilter.Split(',')
            .Select(s => Enum.TryParse<RequisitionType>(s, out var requisitionType) ? requisitionType : (RequisitionType?)null)
            .Where(g => g.HasValue)
            .Select(g => g.Value)
            .ToHashSet();
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

            public Handler(IRequisitionRepository requisitionRepository)
            {
                _requisitionRepository = requisitionRepository;
            }
            public async Task<PaginatedList<RequisitionResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageRequest = new PageRequest
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    IsAscending = request.IsAscending,
                    Keyword = request.Keyword,
                    SortBy = request.SortBy
                };
                var requisitions = await _requisitionRepository.GetPaginatedAsync(pageRequest, request.StartDate, request.EndDate, request.ExpenseHead, request.Department, request.RequisitionStatuses, request.RequisitionTypes);
                return requisitions.Adapt<PaginatedList<RequisitionResponse>>();
            }      
        }
    }
}
