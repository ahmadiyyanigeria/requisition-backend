using Application.Queries;
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
        public async Task<IActionResult> CreateRequisition([FromBody] CreateRequisitionCommand command)
        {
            var requisition = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRequisition), new { id = requisition }, requisition);
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisitions([FromQuery]bool? usePaging, [FromQuery] GetPaginatedRequisitions.Query query)
        {
            if(usePaging.HasValue && !usePaging.Value)
            {
                var requisitions = await _mediator.Send(new GetAllRequisitions.Query());
                return Ok(requisitions);
            }

            var paginatedRequisitions = await _mediator.Send(query);
            return Ok(paginatedRequisitions);
        }
    }
}
