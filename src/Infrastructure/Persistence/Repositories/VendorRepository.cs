using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDbContext _context;

        public VendorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vendor> AddAsync(Vendor vendor)
        {
            await _context.AddAsync(vendor);
            return vendor;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Vendors.AnyAsync(n => n.Name == name);
        }

        public async Task<PaginatedList<Vendor>> GetVendors(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Vendors.AsQueryable();

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Vendor>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderBy(r => r.Name);

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }
        public async Task<Vendor?> GetByIdAsync(Guid id)
        {
            return await _context.Vendors.SingleOrDefaultAsync(n => n.VendorId == id);
        }
    }
}
