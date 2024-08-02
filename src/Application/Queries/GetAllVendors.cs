using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetAllVendors
    {
        public record VendorQuery : IRequest<List<string>>
        {
        }

        public class Handler : IRequestHandler<VendorQuery, List<string>>
        {
            private readonly IVendorRepository _vendorRepository;
            public Handler(IVendorRepository vendorRepository)
            {
                _vendorRepository = vendorRepository;
            }

            public async Task<List<string>> Handle(VendorQuery request, CancellationToken cancellationToken)
            {
                var vendors = await _vendorRepository.GetAllAsync();
                var response = vendors.Select(n => n.Name).ToList();
                return response;
            }   
        }
    }
}
