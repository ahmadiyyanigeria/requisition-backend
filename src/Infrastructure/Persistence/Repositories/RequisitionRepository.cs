using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RequisitionRepository : IRequisitionRepository
    {
        private readonly ApplicationDbContext _context;

        public RequisitionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Requisition> AddAsync(Requisition requisition)
        {
            await _context.Requisitions.AddAsync(requisition);
            return requisition;
        }

        public async Task<Requisition> UpdateAsync(Requisition requisition)
        {
            _context.Requisitions.Update(requisition);
            return requisition;
        }

        public async Task<IReadOnlyList<Requisition>> GetAllAsync()
        {
            return await _context.Requisitions.ToListAsync();
        }

        public async Task<Requisition?> GetByIdAsync(Guid requisitionId)
        {
            return await _context.Requisitions
                .Include(r => r.Items)
                .Include(r => r.Attachments)
                .Include(r => r.ApprovalFlow)
                .ThenInclude(r => r.ApproverSteps)
                .FirstOrDefaultAsync(r => r.RequisitionId == requisitionId);
        }

        public async Task<IReadOnlyList<Requisition>> GetRequisitionsBySubmitterAsync(Guid submitterId)
        {
            return await _context.Requisitions
                .Where(r => r.SubmitterId == submitterId)
                .ToListAsync();
        }
    }
}
