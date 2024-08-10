using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateCashAdvance;
using static Application.Commands.DisburseCashAdvance;
using static Application.Commands.RefundCashAdvance;
using static Application.Commands.ReimburseCashAdvance;
using static Application.Commands.RetireCashAdvance;

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

        [HttpPatch("{id}/disburse")]
        public async Task<IActionResult> DisburseCashAdvance([FromRoute] Guid id, [FromBody] DisburseCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPatch("{id}/retire")]
        public async Task<IActionResult> RetireCashAdvance([FromRoute] Guid id, [FromBody] RetireCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPost("{id}/refund")]
        public async Task<IActionResult> AddRefundEntry([FromRoute] Guid id, [FromBody] RefundCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPost("{id}/reimbursement")]
        public async Task<IActionResult> AddReimbursementEntry([FromRoute] Guid id, [FromBody]ReimburseCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }
    }
}
