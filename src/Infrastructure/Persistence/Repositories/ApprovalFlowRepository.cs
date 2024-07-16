using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ApprovalFlowRepository : IApprovalFlowRepository
    {
        private readonly ApplicationDbContext _context;

        public ApprovalFlowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApprovalFlow> AddAsync(ApprovalFlow approvalFlow)
        {
            await _context.ApprovalFlows.AddAsync(approvalFlow);
            return approvalFlow;
        }

        public async Task<ApprovalFlow?> GetByIdAsync(Guid approvalFlowId)
        {
            return await _context.ApprovalFlows.FirstOrDefaultAsync(af => af.ApprovalFlowId == approvalFlowId);
        }
    }
}
