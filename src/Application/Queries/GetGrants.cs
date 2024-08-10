using Application.Repositories;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetGrants
    {
        public record Query : IRequest<List<GrantResponse>>
        {
           
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

        public class Handler : IRequestHandler<Query, List<GrantResponse>>
        {
            private readonly IGrantRepository _grantRepository;

            public Handler(IGrantRepository grantRepository)
            {
                _grantRepository = grantRepository;
            }
            public async Task<List<GrantResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var grants = await _grantRepository.GetAllAsync();
                return grants.Adapt<List<GrantResponse>>();
            }
        }
    }
}
