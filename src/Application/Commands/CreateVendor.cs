using Application.Exceptions;
using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public class CreateVendor
    {
        public class CreateVendorCommand : IRequest<Guid>
        {
            public string Name { get; init; } = default!;
            public string Address { get; init; } = default!;
            public string ContactPerson { get; init; } = default!;
            public string Email { get; init; } = default!;
            public string PhoneNumber { get; init; } = default!;
        }

        public class Handler : IRequestHandler<CreateVendorCommand, Guid>
        {
            private readonly IVendorRepository _vendorRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
            {
               _vendorRepository = vendorRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
            {
                var vendorExist = await _vendorRepository.ExistAsync(request.Name);
                if(vendorExist)
                {
                    throw new ApplicationException("Vendor already exist", ExceptionCodes.VendorAlreadyExist.ToString(), 400);
                }
                //creating the Vendor object
                var vendor = new Vendor(request.Name, request.Address, request.ContactPerson, request.Email, request.PhoneNumber);

                await _vendorRepository.AddAsync(vendor);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return vendor.VendorId;
            }
        }
    }
}
