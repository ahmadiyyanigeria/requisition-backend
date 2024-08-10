using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Entities.Aggregates.SubmitterAggregate;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public  class CreateCashAdvance
    {
        public class CreateCashAdvanceCommand : IRequest<CashAdvanceResponse>
        {
            public Guid RequisitionId { get; init; }
            public string Notes { get; init; } = default!;
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount, string Notes);

        public class Handler : IRequestHandler<CreateCashAdvanceCommand, CashAdvanceResponse>
        {
            private readonly ICashAdvanceRepository _cashAdvanceRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ICurrentUser _user;

            public Handler(ICashAdvanceRepository cashAdvanceRepository, IUnitOfWork unitOfWork, IRequisitionRepository requisitionRepository, ICurrentUser user)
            {
                _cashAdvanceRepository = cashAdvanceRepository;
                _unitOfWork = unitOfWork;
                _requisitionRepository = requisitionRepository; 
                _user = user;
            }

            public async Task<CashAdvanceResponse> Handle(CreateCashAdvanceCommand request, CancellationToken cancellationToken)
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
                if (requisition.BankAccount is null)
                {
                    throw new ApplicationException($"Bank account details not provided.", ExceptionCodes.BankDetailsNotProvided.ToString(), 400);
                }

                var cashAdvance = new CashAdvance(request.RequisitionId, submitter.SubmitterId, request.Notes, requisition.TotalAmount, requisition.BankAccount!);

                requisition.SetRequisitionProcessed(requisition.RequisitionType);

                await _cashAdvanceRepository.AddAsync(cashAdvance);
                await _requisitionRepository.UpdateAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cashAdvance.Adapt<CashAdvanceResponse>(); 
            }
        }
    }
}
