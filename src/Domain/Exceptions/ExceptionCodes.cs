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
    CashAdvanceReimbursementNotFound,
    CashAdvanceRetired,
    CashAdvanceReimbursementPaid,
    CashAdvanceNotDisbursed,
    CashAdvanceNotInRequestState,
    InvalidRefundAmount,
    InvalidReimbursementAmount,
    RejectNotesNull,
    PurchaseOrderNotFound
}