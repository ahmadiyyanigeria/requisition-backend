
using Application.Repositories;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Queries
{
    public class GetCashAdvance
    {
        public record Query : IRequest<CashAdvanceResponse>
        {
            public Guid Id { get; set; }
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount);

        public class Handler : IRequestHandler<Query, CashAdvanceResponse>
        {
            private readonly ICashAdvanceRepository _cashAdvanceRepository;

            public Handler(ICashAdvanceRepository cashAdvanceRepository)
            {
                _cashAdvanceRepository = cashAdvanceRepository;
            }

            public async Task<CashAdvanceResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Cash Advance not found", ExceptionCodes.CashAdvanceNotFound.ToString(), 404
                   );
                return cashAdvance.Adapt<CashAdvanceResponse>();
            }
        }
    }
}
  
