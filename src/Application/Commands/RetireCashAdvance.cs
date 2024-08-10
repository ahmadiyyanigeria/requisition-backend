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
    public  class RetireCashAdvance
    {
        public class RetireCashAdvanceCommand : IRequest<CashAdvanceResponse>
        {
            public Guid CashAdvanceId { get; set; }
            public string Description { get; init; } = default!;
            public decimal Amount { get; init; }
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount, string Notes);

        public class Handler : IRequestHandler<RetireCashAdvanceCommand, CashAdvanceResponse>
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

            public async Task<CashAdvanceResponse> Handle(RetireCashAdvanceCommand request, CancellationToken cancellationToken)
            {
               // var user = _user.GetUserDetails();
                //string department = "Account";

                var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.CashAdvanceId);
                
                if(cashAdvance == null)
                {
                    throw new ApplicationException($"Cash Advance not found.", ExceptionCodes.CashAdvanceNotFound.ToString(), 404);
                }

                if (cashAdvance.Status != CashAdvanceStatus.Disbursed)
                {
                    throw new ApplicationException($"Cash Advance has not been disbursed.", ExceptionCodes.CashAdvanceNotDisbursed.ToString(), 400);
                }

                if (cashAdvance.Status == CashAdvanceStatus.Retired)
                {
                    throw new ApplicationException($"Cash Advance already retired.", ExceptionCodes.CashAdvanceRetired.ToString(), 400);
                }
               
                var entry = new RetirementEntry(request.CashAdvanceId, request.Description, request.Amount);
                cashAdvance.Retire(entry);

                if(cashAdvance.AdvanceAmount == entry.Amount)
                {
                    cashAdvance.Requisition.SetRequisitionClosed();
                }

                else if(cashAdvance.AdvanceAmount < entry.Amount)
                {
                    var reimbursementAmount = entry.Amount - cashAdvance.AdvanceAmount;
                    var reimbursement = new ReimbursementEntry(request.CashAdvanceId, request.Description, reimbursementAmount);

                    cashAdvance.AddReimbursementEntry(reimbursement);
                }
                else if (cashAdvance.AdvanceAmount > entry.Amount)
                {
                    var refundAmount = cashAdvance.AdvanceAmount - entry.Amount;
                    var refund = new RefundEntry(request.CashAdvanceId, refundAmount);

                    cashAdvance.AddRefundEntry(refund);
                }

                await _cashAdvanceRepository.UpdateAsync(cashAdvance);
                await _requisitionRepository.UpdateAsync(cashAdvance.Requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cashAdvance.Adapt<CashAdvanceResponse>(); 
            }
        }
    }
}
