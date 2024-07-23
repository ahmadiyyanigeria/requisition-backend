using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder)
        {
            await _context.PurchaseOrders.AddAsync(purchaseOrder);
            return purchaseOrder;
        }

        public async Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId)
        {
            return await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.PurchaseOrderId  == purchaseOrderId);
        }
    }
}
