using Application.Repositories;
using Domain.Entities.Aggregates.GrantAggregate;
using Domain.Entities.Common;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class GenerateGrant
    {
        public class GenerateGrantCommand : IRequest<string>
        {
            public Guid GrantId { get; private set; }
            public Guid RequisitionId { get; private set; }
            public Guid SubmitterId { get; private set; }
            public decimal GrantAmount { get; private set; }
            public DateTime? DisbursedDate { get; private set; }
            public BankAccount BankAccount { get; private set; } = default!;
        }

        public class Handler : IRequestHandler<GenerateGrantCommand, string>
        {
            private readonly IGrantRepository _grantRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IGrantRepository grantRepository, IUnitOfWork unitOfWork)
            {
                _grantRepository = grantRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<string> Handle(GenerateGrantCommand request, CancellationToken cancellationToken)
            {
                var grant = new Grant(request.RequisitionId, request.SubmitterId,request.GrantAmount,request.BankAccount);

                await _grantRepository.AddAsync(grant);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return grant.Status.ToString();
            }
        }
    }
}
