using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateCashAdvance;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/cash-advances")]
    [ApiController]
    public class CashAdvancesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CashAdvancesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCashAdvance([FromBody] CreateCashAdvanceCommand command)
        {
            var cashAdvance = await _mediator.Send(command);
            return CreatedAtAction(nameof(GenerateCashAdvance), new { id = cashAdvance }, cashAdvance);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCashAdvance(Guid id)
        {
            var request = await _mediator.Send(new GetCashAdvance.Query { Id = id });
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetCashAdvances([FromQuery] GetCashAdvances.Query query)
        {
            var cashAdvances = await _mediator.Send(query);
            return Ok(cashAdvances);
        }
    }
}
