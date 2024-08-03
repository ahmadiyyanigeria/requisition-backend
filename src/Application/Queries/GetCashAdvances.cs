using Application.Paging;
using Application.Repositories;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetCashAdvances
    {
        public record Query : PageRequest, IRequest<PaginatedList<CashAdvanceResponse>>
        {
            public bool UsePaging { get; init; } = true;
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount);

        public class Handler : IRequestHandler<Query, PaginatedList<CashAdvanceResponse>>
        {
            private readonly ICashAdvanceRepository _cashAdvanceRepository;

            public Handler(ICashAdvanceRepository cashAdvanceRepository)
            {
                _cashAdvanceRepository = cashAdvanceRepository;
            }
            public async Task<PaginatedList<CashAdvanceResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cashAdvances = await _cashAdvanceRepository.GetCashAdvances(request, request.UsePaging);
                return cashAdvances.Adapt<PaginatedList<CashAdvanceResponse>>();
            }
        }
    }
}
