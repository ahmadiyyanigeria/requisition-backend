using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.CreateVendor;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] CreateVendorCommand command)
        {
            var vendor = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateVendor), new { name = vendor }, vendor);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendors([FromQuery] GetAllVendors.VendorQuery query)
        {
            var vendors = await _mediator.Send(query);
            return Ok(vendors);
        }
    }
}
