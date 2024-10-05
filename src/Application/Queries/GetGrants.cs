using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Paging;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetGrants
    {
        public record Query(bool UsePaging = true) : PageRequest, IRequest<List<GrantResponse>>;


        public record GrantResponse(Guid GrantId, Guid RequisitionId, Guid ProcessorId, string Notes, decimal GrantAmount, GrantStatus Status, BankAccount BankAccount);

        public class Handler : IRequestHandler<Query, List<GrantResponse>>
        {
            private readonly IGrantRepository _grantRepository;

            public Handler(IGrantRepository grantRepository)
            {
                _grantRepository = grantRepository;
            }
            public async Task<List<GrantResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var grants = await _grantRepository.GetGrants(request, request.UsePaging);
                return grants.Adapt<List<GrantResponse>>();
            }
        }
    }
}
