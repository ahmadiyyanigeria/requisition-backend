using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Authentication
{
    public static class MockJwtTokens
    {
        public static string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes("S0M3RAN0MS3CR3T!1!MAG1C!1!343456y674688847"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var allClaims = new List<Claim>(claims);

            var token = new JwtSecurityToken(
                issuer: "https://your-keycloak-domain/auth/realms/your-realm",
                audience: "your-client-id",
                claims: allClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
