using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CancelRequisition
    {
        public class CancelRequisitionCommand : IRequest<bool>
        {
            public Guid RequisitionId { get; init; }
        }

        public class Handler : IRequestHandler<CancelRequisitionCommand, bool>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly ISubmitterRepository _submitterRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUser _user;

            public Handler(
                IRequisitionRepository requisitionRepository,
                ISubmitterRepository submitterRepository,
                IUnitOfWork unitOfWork,
                ICurrentUser user)
            {
                _requisitionRepository = requisitionRepository;
                _submitterRepository = submitterRepository;
                _unitOfWork = unitOfWork;
                _user = user;
            }

            public async Task<bool> Handle(CancelRequisitionCommand request, CancellationToken cancellationToken)
            {
                var requisition = await _requisitionRepository.GetByIdAsync(request.RequisitionId);
                if (requisition == null || (requisition.Status != RequisitionStatus.Draft && requisition.Status != RequisitionStatus.Pending))
                {
                    throw new InvalidOperationException("Requisition is either null or not in a state that can be canceled.");
                }
                var user = _user.GetUserDetails();
                if (requisition.SubmitterId.ToString() != user.UserId)
                {
                    throw new UnauthorizedAccessException("You do not have permission to cancel this requisition.");
                }
                requisition.SetRequisitionCanceled();
                _requisitionRepository.UpdateAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        public class CommandValidator : AbstractValidator<CancelRequisitionCommand>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RequisitionId)
                    .NotEmpty().WithMessage("Requisition ID is required.");
            }
        }
    }
}
