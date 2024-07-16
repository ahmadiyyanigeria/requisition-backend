using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace IntegrationTests.Shared.Mocks;
public class MockJwtToken
{
    public List<Claim> Claims { get; } = new();
    public int ExpiresInMinutes { get; set; } = 30;
    public MockJwtToken AddClaims(Guid id, string role)
    {
        Claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
        Claims.Add(new Claim(ClaimTypes.Role, role));
        return this;
    }
    public string Generate()
    {
        var token = new JwtSecurityToken(
            MockJwtTokenProvider.Issuer,
            MockJwtTokenProvider.Issuer,
            Claims,
            expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
            signingCredentials: MockJwtTokenProvider.SigningCredentials
        );
        return MockJwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
    }
}
