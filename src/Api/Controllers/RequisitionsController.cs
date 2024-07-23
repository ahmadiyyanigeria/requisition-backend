using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateRequisition;

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
        public async Task<IActionResult> Submit([FromBody] CreateRequisitionCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requisitionId = await _mediator.Send(command);
            return Ok(requisitionId);
        }
    }
}
