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
    public  class RefundCashAdvance
    {
        public class RefundCashAdvanceCommand : IRequest<CashAdvanceResponse>
        {
            public Guid CashAdvanceId { get; set; }
            public string Description { get; init; } = default!;
            public decimal Amount { get; init; }
            public BankAccount BankAccount { get; init; } = default!;
        }

        public record CashAdvanceResponse(Guid CashAdvanceId, Guid RequisitionId, Guid SubmitterId, decimal AdvanceAmount, CashAdvanceStatus Status, BankAccount BankAccount, string Notes);

        public class Handler : IRequestHandler<RefundCashAdvanceCommand, CashAdvanceResponse>
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

            public async Task<CashAdvanceResponse> Handle(RefundCashAdvanceCommand request, CancellationToken cancellationToken)
            {
                var user = _user.GetUserDetails();
                string department = "Account";

                var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.CashAdvanceId);

                if (cashAdvance == null || cashAdvance.RefundEntry == null)
                {
                    throw new ApplicationException($"Cash Advance refund entry not found.", ExceptionCodes.CashAdvanceNotFound.ToString(), 404);
                }

                if (cashAdvance.RefundEntry.Status == RefundStatus.Paid)
                {
                    throw new ApplicationException($"Refund for the cash advance already paid.", ExceptionCodes.CashAdvanceRetired.ToString(), 400);
                };

                if (cashAdvance.RefundEntry.Amount != request.Amount)
                {
                    throw new ApplicationException($"Amount refunded not tally with refund amount.", ExceptionCodes.InvalidRefundAmount.ToString(), 400);
                }

                cashAdvance.RefundEntry.SetRefundPaid(request.BankAccount);
                cashAdvance.Requisition.SetRequisitionClosed();
                
                await _cashAdvanceRepository.UpdateAsync(cashAdvance);
                await _requisitionRepository.UpdateAsync(cashAdvance.Requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cashAdvance.Adapt<CashAdvanceResponse>();
            }
        }
    }
}
