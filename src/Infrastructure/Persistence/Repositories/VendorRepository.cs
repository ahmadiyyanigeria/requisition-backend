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

        public async Task<IReadOnlyList<Vendor>> GetAllAsync()
        {
           return await _context.Vendors.ToListAsync();
        }

        public async Task<Vendor?> GetByIdAsync(Guid id)
        {
            return await _context.Vendors.SingleOrDefaultAsync(n => n.VendorId == id);
        }
    }
}
