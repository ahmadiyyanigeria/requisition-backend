using Application.Paging;
using Application.Repositories;
using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Domain.Enums;
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

        public async Task<PaginatedList<PurchaseOrder>> GetPurchaseOrders(PageRequest pageRequest, bool usePaging = true, DateTime? orderStartDate = null, DateTime? orderEndDate = null, DateTime? deliveryStartDate = null, DateTime? deliveryEndDate = null,decimal? minTotalAmount = null, decimal? maxTotalAmount = null, PurchaseOrderStatus? status = null, Guid? vendorId = null)
        {
            var query = _context.PurchaseOrders.Include(x => x.Items).Include(x => x.Vendor).AsQueryable();

            if (orderStartDate.HasValue)
            {
                query = query.Where(x => x.OrderDate >= orderStartDate.Value);
            }

            if (orderEndDate.HasValue)
            {
                query = query.Where(x => x.OrderDate <= orderEndDate.Value);
            }

            if (deliveryStartDate.HasValue)
            {
                query = query.Where(x => x.DeliveryDate >= deliveryStartDate.Value);
            }

            if (deliveryEndDate.HasValue)
            {
                query = query.Where(x => x.DeliveryDate <= deliveryEndDate.Value);
            }

            if (minTotalAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount >= minTotalAmount.Value);
            }

            if (maxTotalAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount <= maxTotalAmount.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            if (vendorId.HasValue)
            {
                query = query.Where(x => x.VendorId == vendorId.Value);
            }

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<PurchaseOrder>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderBy(r => r.OrderDate);

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

        public async Task<PurchaseOrder?> GetByIdAsync(Guid purchaseOrderId)
        {
            return await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.PurchaseOrderId  == purchaseOrderId);
        }
    }
}
