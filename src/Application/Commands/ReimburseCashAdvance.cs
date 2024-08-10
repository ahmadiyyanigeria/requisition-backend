using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Commands
{
    public  class ReimburseCashAdvance
    {
        public class ReimburseCashAdvanceCommand : IRequest<Guid>
        {
            public Guid CashAdvanceId { get; set; }
            public string Description { get; init; } = default!;
            public decimal Amount { get; init; }
        }

        public class Handler : IRequestHandler<ReimburseCashAdvanceCommand, Guid>
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

            public async Task<Guid> Handle(ReimburseCashAdvanceCommand request, CancellationToken cancellationToken)
            {
                //var user = _user.GetUserDetails();
                //string department = "Account";

                var cashAdvance = await _cashAdvanceRepository.GetByIdAsync(request.CashAdvanceId);

                if (cashAdvance == null || cashAdvance.ReimbursementEntry == null)
                {
                    throw new ApplicationException($"Cash Advance reimbursement not found.", ExceptionCodes.CashAdvanceReimbursementNotFound.ToString(), 404);
                }

                if (cashAdvance.ReimbursementEntry.Status == ReimbursementStatus.Paid)
                {
                    throw new ApplicationException($"Reimbursement for the cash advance already paid.", ExceptionCodes.CashAdvanceReimbursementPaid.ToString(), 400);
                };

                if (cashAdvance.ReimbursementEntry.Amount != request.Amount)
                {
                    throw new ApplicationException($"Amount reimbursed not tally with reimbursement amount.", ExceptionCodes.InvalidReimbursementAmount.ToString(), 400);
                }

                cashAdvance.ReimbursementEntry.SetReimbursementPaid();
                cashAdvance.Requisition.SetRequisitionClosed();

                await _cashAdvanceRepository.UpdateAsync(cashAdvance);
                await _requisitionRepository.UpdateAsync(cashAdvance.Requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return cashAdvance.ReimbursementEntry.ReimbursementEntryId;
            }
        }
    }
}
