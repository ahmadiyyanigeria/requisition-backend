namespace Domain.Exceptions;

public enum ExceptionCodes
{
    Unauthorized,
    InvalidApprovalState,
    InvalidProcessingState,
    NullApprovalFlow,
    NullRequisitionItem,
    ExpenseHeadAlreadyExist,
    RequisitionNotFound,
    BankDetailsNotProvided,
    CashAdvanceNotFound,
    RejectNotesNull,
    PurchaseOrderNotFound
}