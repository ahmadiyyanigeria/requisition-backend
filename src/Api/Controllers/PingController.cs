using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping()
    {
        var response = "pong";
        return Ok(response);
    }
}