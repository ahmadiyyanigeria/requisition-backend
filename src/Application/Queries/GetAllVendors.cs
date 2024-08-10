using Application.Paging;
using Application.Repositories;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetAllVendors
    {
        public record VendorQuery : PageRequest, IRequest<PaginatedList<VendorResponse>>
        {
            public bool UsePaging { get; init; } = true;
        }

        public record VendorResponse(Guid VendorId, string Name, string Address, string ContactPerson, string ContactEmail, string ContactPhone);

        public class Handler : IRequestHandler<VendorQuery, PaginatedList<VendorResponse>>
        {
            private readonly IVendorRepository _vendorRepository;
            public Handler(IVendorRepository vendorRepository)
            {
                _vendorRepository = vendorRepository;
            }

            public async Task<PaginatedList<VendorResponse>> Handle(VendorQuery request, CancellationToken cancellationToken)
            {
                var vendors = await _vendorRepository.GetVendors(request, request.UsePaging);
                return vendors.Adapt<PaginatedList<VendorResponse>>();
            }   
        }
    }
}
