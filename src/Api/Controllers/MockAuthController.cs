using Api.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockAuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult GetToken([FromBody] string email)
        {
            var user = MockUsers.GetUserByEmail(email);
            if (user == null)
            {
                return Unauthorized("Invalid email address");
            }

            var token = MockJwtTokens.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
