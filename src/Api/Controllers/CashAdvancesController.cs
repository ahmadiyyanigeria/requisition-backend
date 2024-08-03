using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreatePurchaseOrder;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/cash-advances")]
    [ApiController]
    public class PCashAdvancesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PCashAdvancesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCashAdvance(Guid id)
        {
            var request = await _mediator.Send(new GetCashAdvance.Query { Id = id });
            return Ok(request);
        }
    }
}
