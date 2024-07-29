
using Application.Repositories;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Queries
{
   
    namespace Application.Queries
    {
        public class GetCashAdvanceById
        {
            public record Query(Guid Id) : IRequest<CashAdvanceResponse>;
            public record CashAdvanceResponse(Guid Id, Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status);


            public class Handler : IRequestHandler<Query, CashAdvanceResponse>
            {
                private readonly ICashAdvanceRepository _cashAdvanceRepository;

                public Handler(ICashAdvanceRepository cashAdvanceRepository)
                {
                    _cashAdvanceRepository = cashAdvanceRepository;
                }

                public async Task<CashAdvanceResponse> Handle(Query request, CancellationToken cancellationToken)
                {
                    var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.Id);
                    if (cashAdvance == null)
                    {
                            throw new DomainException(
                               message: "CashAdvance not found",
                               errorCode: ExceptionCodes.Unauthorized.ToString(),
                               statusCode: 404
                           );
                        
                    }

                    return cashAdvance.Adapt<CashAdvanceResponse>();
                }
            }
        }
    }
}
