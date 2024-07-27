using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetAllRequisitions
    {
        public record Query : IRequest<List<RequisitionResponse>>
        {
           
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

        public class Handler : IRequestHandler<Query, List<RequisitionResponse>>
        {
            private readonly IRequisitionRepository _requisitionRepository;

            public Handler(IRequisitionRepository requisitionRepository)
            {
                _requisitionRepository = requisitionRepository;
            }
            public async Task<List<RequisitionResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var requisitions = await _requisitionRepository.GetAllAsync();
                return requisitions.Adapt<List<RequisitionResponse>>();
            }
        }
    }
}
