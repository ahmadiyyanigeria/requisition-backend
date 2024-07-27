using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginatedList<Requisition>> GetPaginatedAsync(PageRequest pageRequest, DateTime? startDate, DateTime? endDate, string? expenseHead, string? department, HashSet<RequisitionStatus>? statusFilter, HashSet<RequisitionType>? typeFilter)
        {
            var query = _context.Requisitions.Include(r => r.Submitter).AsQueryable();
            // Apply Search 
            if (!string.IsNullOrWhiteSpace(pageRequest?.Keyword))
            {
                var stringProperties = typeof(Requisition).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var property in stringProperties)
                {
                    query = query.Where(r => EF.Functions.Like(EF.Property<string>(r, property.Name), $"%{pageRequest.Keyword}%"));
                }
            }

            // Additional filters based on method parameters
            if (startDate.HasValue && startDate.Value != DateTime.MinValue)
                query = query.Where(r => r.RequestedDate.Date >= startDate.Value.Date);
            if (endDate.HasValue && endDate.Value != DateTime.MinValue)
                query = query.Where(r => r.RequestedDate.Date <= endDate.Value.Date);
            if (!string.IsNullOrWhiteSpace(expenseHead))
                query = query.Where(r => EF.Functions.Like(r.ExpenseHead, expenseHead));
            if (!string.IsNullOrWhiteSpace(department))
                query = query.Where(r => EF.Functions.Like(r.Department, department));
            if (statusFilter?.Any() ?? false)
            {
                query = query.Where(r => statusFilter.Contains(r.Status));
            }
            if (typeFilter?.Any() ?? false)
            {
                query = query.Where(r => typeFilter.Contains(r.RequisitionType));
            }

            //Apply Sorting
            var _sortByMappings = new Dictionary<string, Expression<Func<Requisition, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                {"Date", r => r.RequestedDate },
                {"RequisitionType", r => r.RequisitionType },
                {"Status", r => r.Status },
                {"Category", r => r.ExpenseHead },
                {"RequestedBy", r => r.Submitter.Name },
                {"Department", r => r.Department }
            };

            var sortBy = !string.IsNullOrWhiteSpace(pageRequest.SortBy) && _sortByMappings.ContainsKey(pageRequest.SortBy) ? pageRequest.SortBy : "Date";
            var sortByExpression = _sortByMappings[sortBy];
            query = pageRequest.IsAscending
                ? query.OrderBy(sortByExpression)
                : query.OrderByDescending(sortByExpression);

            var totalItemsCount = await query.CountAsync();
            var skip = (pageRequest.Page - 1) * pageRequest.PageSize;
            var result = await query.Skip(skip).Take(pageRequest.PageSize).ToListAsync();
            return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
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
