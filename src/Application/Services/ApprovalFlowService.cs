﻿using Application.Configurations;
using Domain.Constants;
using Domain.Entities.Aggregates.RequisitionAggregate;
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

            var approvalFlow = new ApprovalFlow(requisition.RequisitionId, approvers);

            foreach (var role in approvalSteps)
            {
                var approverId = GetRoleUserId(role); // Method to get user ID for the role
                approvers.AddLast(new ApprovalStep(approvalFlow.ApprovalFlowId, approverId, [role]));
            }

            return approvalFlow;
        }

        private string GetRoleUserId(string role)
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
