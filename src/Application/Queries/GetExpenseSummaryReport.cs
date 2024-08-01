using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;
using MediatR;

namespace Application.Queries
{
    public class GetExpenseSummaryReport
    {
        public record Query(DateTime? StartDate, DateTime? EndDate) : IRequest<ExpenseSummaryReport>;

        public record ExpenseSummaryReport(
            int TotalRequests,
            int TotalApproved,
            int TotalRejected,
            int TotalInProgress,
            int TotalPending,
            int TotalProcessed,
            Dictionary<string, int> RequestsByTypes,
            Dictionary<string, int> RequestsByDepartments,
            Dictionary<string, int> RequestsByCategories,
            Dictionary<string, int> RequestsApprovedByTypes,
            Dictionary<string, int> RequestsApprovedByDepartments,
            Dictionary<string, int> RequestsApprovedByCategories,
            Dictionary<string, int> RequestsRejectedByTypes,
            Dictionary<string, int> RequestsRejectedByDepartments,
            Dictionary<string, int> RequestsRejectedByCategories,
            Dictionary<string, int> RequestsPendingByTypes,
            Dictionary<string, int> RequestsPendingByDepartments,
            Dictionary<string, int> RequestsPendingByCategories,
            Dictionary<string, int> RequestsProcessedByTypes,
            Dictionary<string, int> RequestsProcessedByDepartments,
            Dictionary<string, int> RequestsProcessedByCategories,
            Dictionary<string, int> RequestsInProgressByTypes,
            Dictionary<string, int> RequestsInProgressByDepartments,
            Dictionary<string, int> RequestsInProgressByCategories,
            decimal TotalAmountRequested,
            decimal TotalAmountApproved,
            Dictionary<string, decimal> AmountRequestedByTypes,
            Dictionary<string, decimal> AmountApprovedByTypes,
            Dictionary<string, decimal> AmountRequestedByCategories,
            Dictionary<string, decimal> AmountApprovedByCategories,
            Dictionary<string, decimal> AmountRequestedByDepartments,
            Dictionary<string, decimal> AmountApprovedByDepartments,
            double AverageApprovalTime,
            Dictionary<string, double> ApprovalTimesByTypes,
            Dictionary<string, double> ApprovalTimesByCategories,
            Dictionary<string, double> ApprovalTimesByDepartments
        );

        public class Handler : IRequestHandler<Query, ExpenseSummaryReport>
        {
            private readonly IRequisitionRepository _requisitionRepository;

            public Handler(IRequisitionRepository requisitionRepository)
            {
                _requisitionRepository = requisitionRepository;
            }

            public async Task<ExpenseSummaryReport> Handle(Query request, CancellationToken cancellationToken)
            {
                var expenses = await _requisitionRepository.GetAllAsync(request.StartDate, request.EndDate);

                var totalRequests = expenses.Count;
                var totalApprovedRequests = GetCountByStatus(expenses, RequisitionStatus.Approved);
                var totalRejectedRequests = GetCountByStatus(expenses, RequisitionStatus.Rejected);
                var totalInProgressRequests = GetCountByStatus(expenses, RequisitionStatus.InProgress);
                var totalPendingRequests = GetCountByStatus(expenses, RequisitionStatus.Pending);
                var totalProcessedRequests = GetCountByStatus(expenses, RequisitionStatus.Processed);

                var expenseCountByTypes = GroupCountBy(expenses, e => e.RequisitionType.ToString());
                var expenseCountByCategories = GroupCountBy(expenses, e => e.ExpenseHead.ToString());
                var expenseCountByDepartments = GroupCountBy(expenses, e => e.Department.ToString());

                var expenseApprovedCountByTypes = GroupCountByStatus(expenses, RequisitionStatus.Approved, e => e.RequisitionType.ToString());
                var expenseApprovedCountByCategories = GroupCountByStatus(expenses, RequisitionStatus.Approved, e => e.ExpenseHead.ToString());
                var expenseApprovedCountByDepartments = GroupCountByStatus(expenses, RequisitionStatus.Approved, e => e.Department.ToString());

                var expensePendingCountByTypes = GroupCountByStatus(expenses, RequisitionStatus.Pending, e => e.RequisitionType.ToString());
                var expensePendingCountByCategories = GroupCountByStatus(expenses, RequisitionStatus.Pending, e => e.ExpenseHead.ToString());
                var expensePendingCountByDepartments = GroupCountByStatus(expenses, RequisitionStatus.Pending, e => e.Department.ToString());

                var expenseInProgressCountByTypes = GroupCountByStatus(expenses, RequisitionStatus.InProgress, e => e.RequisitionType.ToString());
                var expenseInProgressCountByCategories = GroupCountByStatus(expenses, RequisitionStatus.InProgress, e => e.ExpenseHead.ToString());
                var expenseInProgressCountByDepartments = GroupCountByStatus(expenses, RequisitionStatus.InProgress, e => e.Department.ToString());

                var expenseRejectedCountByTypes = GroupCountByStatus(expenses, RequisitionStatus.Rejected, e => e.RequisitionType.ToString());
                var expenseRejectedCountByCategories = GroupCountByStatus(expenses, RequisitionStatus.Rejected, e => e.ExpenseHead.ToString());
                var expenseRejectedCountByDepartments = GroupCountByStatus(expenses, RequisitionStatus.Rejected, e => e.Department.ToString());

                var expenseProcessedCountByTypes = GroupCountByStatus(expenses, RequisitionStatus.Processed, e => e.RequisitionType.ToString());
                var expenseProcessedCountByCategories = GroupCountByStatus(expenses, RequisitionStatus.Processed, e => e.ExpenseHead.ToString());
                var expenseProcessedCountByDepartments = GroupCountByStatus(expenses, RequisitionStatus.Processed, e => e.Department.ToString());

                var totalAmountRequested = expenses.Sum(e => e.TotalAmount);
                var totalAmountApproved = expenses.Where(e => e.Status == RequisitionStatus.Approved).Sum(e => e.TotalAmount);

                var amountRequestedByTypes = GroupSumBy(expenses, e => e.RequisitionType.ToString(), e => e.TotalAmount);
                var amountApprovedByTypes = GroupSumBy(expenses.Where(e => e.Status == RequisitionStatus.Approved), e => e.RequisitionType.ToString(), e => e.TotalAmount);
                var amountRequestedByCategories = GroupSumBy(expenses, e => e.ExpenseHead.ToString(), e => e.TotalAmount);
                var amountApprovedByCategories = GroupSumBy(expenses.Where(e => e.Status == RequisitionStatus.Approved), e => e.ExpenseHead.ToString(), e => e.TotalAmount);
                var amountRequestedByDepartments = GroupSumBy(expenses, e => e.Department.ToString(), e => e.TotalAmount);
                var amountApprovedByDepartments = GroupSumBy(expenses.Where(e => e.Status == RequisitionStatus.Approved), e => e.Department.ToString(), e => e.TotalAmount);

                var approvalTimes = expenses.Where(x => x.Status == RequisitionStatus.Approved || x.Status == RequisitionStatus.Processed || x.Status == RequisitionStatus.Rejected);
                var averageApprovalTimes = approvalTimes.Any()
                    ? approvalTimes.Average(a => (a.ApprovedDate - a.RequestedDate).Value.TotalDays)
                    : 0;

                var approvalTimesByTypes = GroupAverageApprovalTimes(approvalTimes, e => e.RequisitionType.ToString());
                var approvalTimesByCategories = GroupAverageApprovalTimes(approvalTimes, e => e.ExpenseHead.ToString());
                var approvalTimesByDepartments = GroupAverageApprovalTimes(approvalTimes, e => e.Department.ToString());

                return new ExpenseSummaryReport(
                    totalRequests,
                    totalApprovedRequests,
                    totalRejectedRequests,
                    totalInProgressRequests,
                    totalPendingRequests,
                    totalProcessedRequests,
                    expenseCountByTypes,
                    expenseCountByDepartments,
                    expenseCountByCategories,
                    expenseApprovedCountByTypes,
                    expenseApprovedCountByDepartments,
                    expenseApprovedCountByCategories,
                    expenseRejectedCountByTypes,
                    expenseRejectedCountByDepartments,
                    expenseRejectedCountByCategories,
                    expensePendingCountByTypes,
                    expensePendingCountByDepartments,
                    expensePendingCountByCategories,
                    expenseProcessedCountByTypes,
                    expenseProcessedCountByDepartments,
                    expenseProcessedCountByCategories,
                    expenseInProgressCountByTypes,
                    expenseInProgressCountByDepartments,
                    expenseInProgressCountByCategories,
                    totalAmountRequested,
                    totalAmountApproved,
                    amountRequestedByTypes,
                    amountApprovedByTypes,
                    amountRequestedByCategories,
                    amountApprovedByCategories,
                    amountRequestedByDepartments,
                    amountApprovedByDepartments,
                    averageApprovalTimes,
                    approvalTimesByTypes,
                    approvalTimesByCategories,
                    approvalTimesByDepartments
                );
            }

            private int GetCountByStatus(IEnumerable<Requisition> expenses, RequisitionStatus status)
                => expenses.Count(x => x.Status == status);

            private Dictionary<string, int> GroupCountBy(IEnumerable<Requisition> expenses, Func<Requisition, string> keySelector)
                => expenses.GroupBy(keySelector)
                            .ToDictionary(g => g.Key, g => g.Count());

            private Dictionary<string, int> GroupCountByStatus(IEnumerable<Requisition> expenses, RequisitionStatus status, Func<Requisition, string> keySelector)
                => expenses.Where(e => e.Status == status)
                            .GroupBy(keySelector)
                            .ToDictionary(g => g.Key, g => g.Count());

            private Dictionary<string, decimal> GroupSumBy(IEnumerable<Requisition> expenses, Func<Requisition, string> keySelector, Func<Requisition, decimal> amountSelector)
                => expenses.GroupBy(keySelector)
                            .ToDictionary(g => g.Key, g => g.Sum(amountSelector));

            private Dictionary<string, double> GroupAverageApprovalTimes(IEnumerable<Requisition> expenses, Func<Requisition, string> keySelector)
                => expenses.GroupBy(keySelector)
                            .ToDictionary(g => g.Key, g => g.Average(a => (a.ApprovedDate - a.RequestedDate).Value.TotalDays));
        }
    }
}
