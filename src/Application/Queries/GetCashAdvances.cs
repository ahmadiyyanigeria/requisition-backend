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
        public record Query(bool UsePaging = true,
        DateTime? RequestedStartDate = null,
        DateTime? RequestedEndDate = null,
        DateTime? DisbursedStartDate = null,
        DateTime? DisbursedEndDate = null,
        DateTime? RetiredStartDate = null,
        DateTime? RetiredEndDate = null,
        CashAdvanceStatus? Status = null,
        decimal? MinAdvanceAmount = null,
        decimal? MaxAdvanceAmount = null) : PageRequest, IRequest<PaginatedList<CashAdvanceResponse>>;

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
                var cashAdvances = await _cashAdvanceRepository.GetCashAdvances(request, request.UsePaging, request.RequestedStartDate, request.RequestedEndDate, request.DisbursedStartDate, request.DisbursedEndDate, request.RetiredStartDate, request.RetiredEndDate,request.Status, request.MinAdvanceAmount, request.MaxAdvanceAmount);
                return cashAdvances.Adapt<PaginatedList<CashAdvanceResponse>>();
            }
        }
    }
}
