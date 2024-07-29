using Application.Paging;
using Application.Repositories;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetAllCashAdvance
    {
        public record GetAllCashAdvanceResponse(Guid Id, Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status);



        public record GetCashAdvanceQuery : PageRequest, IRequest<List<GetAllCashAdvanceResponse>>
        {
            public Guid CashAdvanceId { get; set; }
            public bool UsePaging { get; init; } = true;

            public GetCashAdvanceQuery(Guid cashAdvanceId, bool usePaging)
            {
                CashAdvanceId = cashAdvanceId;
                UsePaging = usePaging;
            }
        }


        public class GetCashAdvanceQueryHandler : IRequestHandler<GetCashAdvanceQuery, List<GetAllCashAdvanceResponse>>
        {
            private readonly ICashAdvanceRepository _cashAdvanceRepository;

            public GetCashAdvanceQueryHandler(ICashAdvanceRepository cashAdvanceRepository)
            {
                _cashAdvanceRepository = cashAdvanceRepository;
            }

            public async Task<List<GetAllCashAdvanceResponse>> Handle(GetCashAdvanceQuery request, CancellationToken cancellationToken)
            {

                var cashAdvance = await _cashAdvanceRepository.GetAll(request, cashAdvance => cashAdvance.CashAdvanceId == request.CashAdvanceId, request.UsePaging);
                return cashAdvance.Adapt<List<GetAllCashAdvanceResponse>>();

            }
        }
    }
}