using Application.Repositories;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Queries.GetPurchaseOrder;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Queries
{
    public class GetPurchaseOrder : IRequest<PurchaseOrderResponse>
    {
        public record Query : IRequest<PurchaseOrderResponse>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PurchaseOrderResponse>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPurchaseOrderRepository _purchaseOrderRepository;

            public Handler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<Handler> logger)
            {
                _logger = logger;
                _purchaseOrderRepository = purchaseOrderRepository;
            }
            public async Task<PurchaseOrderResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.Id);
                if (purchaseOrder is null)
                {
                    _logger.LogError("Purchase order with Id {Id} does not exist", request.Id);
                    throw new ApplicationException($"Purchase with Id {request.Id} does not exists", ExceptionCodes.PurchaseOrderNotFound.ToString(), 404);
                }
                var purchaseOrderResponse = purchaseOrder.Adapt<PurchaseOrderResponse>();
                return purchaseOrderResponse;

            }
        }
        public record PurchaseOrderResponse(Guid PurchaseOrderId, Guid RequisitionId, DateTime OrderDate, decimal TotalAmount, DateTime DeliveryDate,PurchaseOrderStatus Status);
        
    }
}
