using Application.Repositories;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetAllVendors
    {
        public record VendorQuery : IRequest<List<VendorResponse>>
        {
        }

        public record VendorResponse(Guid VendorId, string Name, string Address, string ContactPerson, string ContactEmail, string ContactPhone);

        public class Handler : IRequestHandler<VendorQuery, List<VendorResponse>>
        {
            private readonly IVendorRepository _vendorRepository;
            public Handler(IVendorRepository vendorRepository)
            {
                _vendorRepository = vendorRepository;
            }

            public async Task<List<VendorResponse>> Handle(VendorQuery request, CancellationToken cancellationToken)
            {
                var vendors = await _vendorRepository.GetAllAsync();
                return vendors.Adapt<List<VendorResponse>>();
            }   
        }
    }
}
