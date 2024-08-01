﻿using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreatePurchaseOrder;
using static Application.Commands.CreateRequisition;

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

        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePurchaseOrder([FromBody] CreatePurchaseOrderCommand command)
        {
            var purchaseOrder = await _mediator.Send(command);
            return CreatedAtAction(nameof(GeneratePurchaseOrder), new { id = purchaseOrder }, purchaseOrder);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPurchaseOrder(Guid id)
        {
            var request = await _mediator.Send(new GetPurchaseOrder.Query { Id = id });
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrders()
        {
            var purchaseOders = await _mediator.Send(new GetAllPurchaseOrders.Query());
            return Ok(purchaseOders);
        }
    }
}
