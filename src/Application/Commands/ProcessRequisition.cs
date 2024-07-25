using Application.Repositories;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands
{
    public class ProcessRequisition
    {
        public class ProcessRequisitionCommand : IRequest<Guid>
        {
            public Guid RequisitionId { get; set; }
            public string? Notes { get; set; }
            public RequisitionAction Action { get; set; }
        }

        public enum RequisitionAction
        {
            Approve,
            Reject
        }

        public class Handler : IRequestHandler<ProcessRequisitionCommand, Guid>
        {
            private readonly IRequisitionRepository _requisitionRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IRequisitionRepository requisitionRepository, IUnitOfWork unitOfWork)
            {
                _requisitionRepository = requisitionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(ProcessRequisitionCommand request, CancellationToken cancellationToken)
            {
                //get approvalId
                string approverId = "1";
                var requisition = await _requisitionRepository.GetByIdAsync(request.RequisitionId) ?? throw new DomainException("Requisition not found", ExceptionCodes.RequisitionNotFound.ToString(), 400);

                // Process the action
                if (request.Action == RequisitionAction.Approve)
                {
                    requisition.ApproveCurrentStep(approverId, request.Notes);
                }
                else if (request.Action == RequisitionAction.Reject)
                {
                    if (string.IsNullOrEmpty(request.Notes))
                    {
                        throw new DomainException($"Notes cannot be null or empty when rejecting a requisition.", ExceptionCodes.RejectNotesNull.ToString(), 400);
                    }
                    requisition.RejectCurrentStep(approverId, request.Notes);
                }

                // Save changes
                await _requisitionRepository.UpdateAsync(requisition);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return requisition.RequisitionId;
            }
        }
    }
}
