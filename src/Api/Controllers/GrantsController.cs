using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.GenerateGrant;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/grants")]
    [ApiController]
    public class GrantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GrantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateGrant([FromBody] GenerateGrantCommand command)
        {
            var grant = await _mediator.Send(command);
            return CreatedAtAction(nameof(GenerateGrant), new { name = grant }, grant);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGrant(Guid id)
        {
            var request = await _mediator.Send(new GetGrant.Query { Id = id });
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetGrants([FromQuery] bool? usePaging, [FromQuery] GetGrants.Query query)
        {
            if (usePaging.HasValue && usePaging.Value)
            {
                var paginatedRequisitions = await _mediator.Send(query);
                return Ok(paginatedRequisitions);
            }

            var grants = await _mediator.Send(new GetGrants.Query());
            return Ok(grants);
        }

    }
}
