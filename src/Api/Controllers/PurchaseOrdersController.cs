using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreatePurchaseOrder;
using static Application.Commands.FulfilPurchaseOrder;
using static Application.Commands.PayPurchaseOrder;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/purchase-orders")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderCommand command)
        {
            var purchaseOrder = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPurchaseOrder), new { id = purchaseOrder }, purchaseOrder);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPurchaseOrder(Guid id)
        {
            var purchaseOrder = await _mediator.Send(new GetPurchaseOrder.Query { Id = id });
            return Ok(purchaseOrder);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrders([FromQuery] GetPurchaseOrders.Query query)
        {
            var purchaseOrders = await _mediator.Send(query);
            return Ok(purchaseOrders);
        }

        [HttpPatch("{id:guid}/fulfilment")]
        public async Task<IActionResult> UpdatePurchaseOrderStatus([FromRoute] Guid id, [FromBody] FulfilPurchaseOrderCommand command)
        {
            command.PurchaseOrderId = id;
            var purchaseOrder = await _mediator.Send(command);
            return Ok(purchaseOrder);
        }

        [HttpPatch("{id:guid}/payment")]
        public async Task<IActionResult> UpdatePurchaseOrderPayment([FromRoute] Guid id, [FromBody] PayPurchaseOrderCommand command)
        {
            command.PurchaseOrderId = id;
            var purchaseOrder = await _mediator.Send(command);
            return Ok(purchaseOrder);
        }
    }
}
