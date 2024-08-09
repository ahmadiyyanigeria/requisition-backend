using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public  class DisburseCashAdvance
    {
        public class DisburseCashAdvanceCommand : IRequest<CashAdvanceResponse>
        {
            public Guid CashAdvanceId { get; set; }
            public string Description { get; init; } = default!;
            public decimal Amount { get; init; }
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount, string Notes);

        public class Handler : IRequestHandler<DisburseCashAdvanceCommand, CashAdvanceResponse>
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

            public async Task<CashAdvanceResponse> Handle(DisburseCashAdvanceCommand request, CancellationToken cancellationToken)
            {
               // var user = _user.GetUserDetails();
                //string department = "Account";

                var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.CashAdvanceId);
                
                if(cashAdvance == null)
                {
                    throw new ApplicationException($"Cash Advance not found.", ExceptionCodes.CashAdvanceNotFound.ToString(), 404);
                }

                if (cashAdvance.Status != CashAdvanceStatus.Requested)
                {
                    throw new ApplicationException($"Cash Advance not in request status.", ExceptionCodes.CashAdvanceNotInRequestState.ToString(), 400);
                }

                if (cashAdvance.Status == CashAdvanceStatus.Retired)
                {
                    throw new ApplicationException($"Cash Advance already retired.", ExceptionCodes.CashAdvanceRetired.ToString(), 400);
                }
               
                cashAdvance.Disburse();

                await _cashAdvanceRepository.UpdateAsync(cashAdvance);
                await _requisitionRepository.UpdateAsync(cashAdvance.Requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cashAdvance.Adapt<CashAdvanceResponse>(); 
            }
        }
    }
}
