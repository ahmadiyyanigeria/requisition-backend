using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Entities.Aggregates.GrantAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.Common;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public class GenerateGrant
    {
        public class GenerateGrantCommand : IRequest<GrantResponse>
        {
            public Guid RequisitionId { get; set; }
            public string Note { get; set; }
        }

        public record GrantResponse(Guid GrantId, Guid RequisitionId, Guid ProcessorId,string Notes, decimal GrantAmount, GrantStatus Status, BankAccount BankAccount);

        public class Handler : IRequestHandler<GenerateGrantCommand, GrantResponse>
        {
            private readonly IGrantRepository _grantRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ICurrentUser _user;

            public Handler(IGrantRepository grantRepository, IUnitOfWork unitOfWork, IRequisitionRepository requisitionRepository, ICurrentUser user)
            {
                _grantRepository = grantRepository;
                _unitOfWork = unitOfWork;
                _requisitionRepository = requisitionRepository;
                _user = user;
            }

            public async Task<GrantResponse> Handle(GenerateGrantCommand request, CancellationToken cancellationToken)
            {
                var user = _user.GetUserDetails();
                string department = "Account";

                //create submitter record
                var submitter = new Submitter(user.UserId, user.Name, user.Email, user.Role, department);

                var requisition = await _requisitionRepository.GetByIdAsync(request.RequisitionId);
                if (requisition is null)
                {
                    throw new ApplicationException($"Requisition not found.", ExceptionCodes.RequisitionNotFound.ToString(), 404);
                }
                if(requisition.BankAccount is null)
                {
                    throw new ApplicationException($"Bank account not provided.", ExceptionCodes.RequisitionNotFound.ToString(), 404);
                }
                var grant = new Grant(request.RequisitionId,submitter.SubmitterId,request.Note,requisition.TotalAmount,requisition.BankAccount);

                requisition.SetRequisitionProcessed(RequisitionType.Grant);
                
                await _grantRepository.AddAsync(grant);
                await _requisitionRepository.UpdateAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return grant.Adapt<GrantResponse>();
            }
        }
    }
}
