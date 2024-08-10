using Application.Repositories;
using Domain.Enums;
using Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Exceptions;
using ApplicationException = Application.Exceptions.ApplicationException;
using Domain.Entities.ValueObjects;
using Mapster;

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

                return grant.Adapt<GrantResponse>();
            }
        }

        public record GrantResponse(Guid GrantId, Guid RequisitionId, Guid ProcessorId, string Notes, decimal GrantAmount, GrantStatus Status, BankAccount BankAccount);
    }
}
