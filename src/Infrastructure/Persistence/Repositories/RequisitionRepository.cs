using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Linq.Expressions;

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
            return await _context.Requisitions
                .Include(r => r.Submitter)
                .ToListAsync();
        }


        public async Task<IReadOnlyList<Requisition>> GetAllAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Requisition> query = _context.Requisitions.Include(r => r.Submitter);

            if (startDate.HasValue)
            {
                query = query.Where(r => r.RequestedDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(r => r.RequestedDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<PaginatedList<Requisition>> GetRequisitions(PageRequest pageRequest, bool usePaging = true, DateTime? requestedStartDate = null, DateTime? requestedEndDate = null, RequisitionStatus? status = null, decimal? minTotalAmount = null, decimal? maxTotalAmount = null, IReadOnlyList<Guid>? submitterIds = null, string? expenseHead = null, RequisitionType? requisitionType = null, string? department = null)
        {
            var query = _context.Requisitions.Include(x => x.ApprovalFlow).AsQueryable();

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Requisition>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            if (requestedStartDate.HasValue)
            {
                query = query.Where(x => x.RequestedDate >= requestedStartDate.Value);
            }

            if (requestedEndDate.HasValue)
            {
                query = query.Where(x => x.RequestedDate <= requestedEndDate.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            if (minTotalAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount >= minTotalAmount.Value);
            }

            if (maxTotalAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount <= maxTotalAmount.Value);
            }

            // Apply submitter ID filter
            if (submitterIds != null && submitterIds.Any())
            {
                query = query.Where(x => submitterIds.Contains(x.SubmitterId));
            }

            if (!string.IsNullOrEmpty(expenseHead))
            {
                query = query.Where(x => EF.Functions.ILike(x.ExpenseHead, $"%{expenseHead}%"));
            }

            if (requisitionType.HasValue)
            {
                query = query.Where(x => x.RequisitionType == requisitionType.Value);
            }

            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(x => EF.Functions.ILike(x.Department, $"%{department}%"));
            }

            query = query.OrderBy(x => x.RequestedDate);

            var totalItemsCount = await query.CountAsync();

            var result = usePaging
                ? await query.Skip((pageRequest.Page - 1) * pageRequest.PageSize).Take(pageRequest.PageSize).ToListAsync()
                : await query.ToListAsync();

            return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
        }

        public async Task<Requisition?> GetByIdAsync(Guid requisitionId)
        {
            return await _context.Requisitions
                .Include(r => r.Submitter)
                .Include(r => r.Items)
                .Include(r => r.Attachments)
                .Include(r => r.ApprovalFlow)
                .ThenInclude(af => af.ApproverSteps.OrderBy(r => r.Order))
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
