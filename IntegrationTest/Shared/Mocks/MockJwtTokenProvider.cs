using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IntegrationTests.Shared.Mocks;

public class MockJwtTokenProvider
{
    public static string Issuer { get; } = "https://test.com";
    public static SecurityKey SecurityKey { get; } = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test-secret-key45654323vcxnmnbvddfgh6543ytdwtytresfghgfjy765tyu"));
    public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    public static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
}