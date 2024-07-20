using Application.Configurations;
using Domain.Constants;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class ApprovalFlowService : IApprovalFlowService
    {
        private readonly IUserService _userService;
        private readonly IOptions<ApprovalFlowConfiguration> _approvalFlowConfig;

        public ApprovalFlowService(IUserService userService, IOptions<ApprovalFlowConfiguration> approvalFlowConfig)
        {
            _userService = userService;
            _approvalFlowConfig = approvalFlowConfig;
        }

        public ApprovalFlow CreateApprovalFlow(Requisition requisition, string submitterRole)
        {
            var approvers = new LinkedList<ApprovalStep>();

            //read the approval flow config from settings
            var approvalSteps = new List<string>(_approvalFlowConfig.Value.Steps);

            if (submitterRole == Roles.HOD)
            {
                // Remove the first approver if the submitter is HOD
                approvalSteps.RemoveAt(0);
            }

            foreach (var role in approvalSteps)
            {
                var approverRole = role;
                approvers.AddLast(new ApprovalStep(GetRoleUserId(approverRole), [approverRole]));
            }

            return new ApprovalFlow(requisition.RequisitionId, approvers);
        }

        private Guid GetRoleUserId(string role)
        {
            var user = _userService.GetUserByRole(role);
            if (user == null)
            {
                throw new InvalidOperationException($"No user found for role {role}");
            }
            return user.UserId;
        }
    }
}
