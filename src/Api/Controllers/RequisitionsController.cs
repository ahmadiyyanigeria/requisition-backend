using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateRequisition;
using static Application.Commands.ProcessRequisition;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/requisitions")]
    [ApiController]
    public class RequisitionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequisitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateRequisition([FromBody] CreateRequisitionCommand command)
        {
            var requisition = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRequisition), new { id = requisition }, requisition);
        }

        [HttpPut("{requisitionId}/process")]
        public async Task<IActionResult> ProcessRequisition([FromRoute] Guid requisitionId, [FromBody] ProcessRequisitionCommand command)
        {
            command.RequisitionId = requisitionId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
