using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateExpenseHead;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/expense-heads")]
    [ApiController]
    public class ExpenseHeadsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseHeadsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseHead([FromBody] CreateExpenseHeadCommand command)
        {
            var expenseHead = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateExpenseHead), new { name = expenseHead }, expenseHead);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenseHeads([FromQuery] GetAllExpenseHeads.Query query)
        {
            var expenseHeads = await _mediator.Send(query);
            return Ok(expenseHeads);
        }
    }
}
