using Application.Repositories;
using Domain.Enums;
using Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Exceptions;
using ApplicationException = Application.Exceptions.ApplicationException;
using Domain.Entities.ValueObjects;

namespace Application.Queries
{
    public class GetGrant
    {
        public record Query : IRequest<GrantResponse>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, GrantResponse>
        {
            private readonly IGrantRepository _grantRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IGrantRepository grantRepository, ILogger<Handler> logger)
            {
                _grantRepository = grantRepository;
                _logger = logger;
            }
            public async Task<GrantResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var grant = await _grantRepository.GetByIdAsync(request.Id);
                if (grant is null)
                {
                    _logger.LogError("Grant with Id {Id} does not exist", request.Id);
                    throw new ApplicationException($"Grant with Id {request.Id} does not exists", ExceptionCodes.GrantNotFound.ToString(), 404);
                }

                var grantResponse = new GrantResponse();

                return grantResponse;
            }
        }

        public record GrantResponse
        {
            public Guid GrantId { get; private set; }
            public Guid RequisitionId { get; private set; }
            public Guid SubmitterId { get; private set; }
            public decimal GrantAmount { get; private set; }
            public DateTime RequestedDate { get; private set; }
            public DateTime? DisbursedDate { get; private set; }
            public BankAccount BankAccount { get; private set; } = default!;
            public GrantStatus Status { get; private set; }
        }
    }
}
