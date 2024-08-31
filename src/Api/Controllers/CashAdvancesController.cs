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

        [HttpPost]
        public async Task<IActionResult> CreateCashAdvance([FromBody] CreateCashAdvanceCommand command)
        {
            var cashAdvance = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCashAdvance), new { id = cashAdvance }, cashAdvance);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCashAdvance(Guid id)
        {
            var cashAdvance = await _mediator.Send(new GetCashAdvance.Query { Id = id });
            return Ok(cashAdvance);
        }

        [HttpGet]
        public async Task<IActionResult> GetCashAdvances([FromQuery] GetCashAdvances.Query query)
        {
            var cashAdvances = await _mediator.Send(query);
            return Ok(cashAdvances);
        }

        [HttpPatch("{id:guid}/disbursement")]
        public async Task<IActionResult> UpdateCashAdvanceDisbursement([FromRoute] Guid id, [FromBody] DisburseCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPatch("{id:guid}/retirement")]
        public async Task<IActionResult> RetireCashAdvance([FromRoute] Guid id, [FromBody] RetireCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPatch("{id:guid}/refund")]
        public async Task<IActionResult> UpdateCashAdvanceRefund([FromRoute] Guid id, [FromBody] RefundCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }

        [HttpPatch("{id:guid}/reimbursement")]
        public async Task<IActionResult> UpdateCashAdvanceReimbursement([FromRoute] Guid id, [FromBody] ReimburseCashAdvanceCommand command)
        {
            command.CashAdvanceId = id;
            var cashAdvance = await _mediator.Send(command);
            return Ok(cashAdvance);
        }
    }
}
