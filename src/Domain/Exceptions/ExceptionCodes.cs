namespace Domain.Exceptions;

public enum ExceptionCodes
{
    Unauthorized,
    InvalidApprovalState,
    InvalidProcessingState,
    NullApprovalFlow,
    NullRequisitionItem,
    RequisitionNotFound,
    RejectNotesNull
}