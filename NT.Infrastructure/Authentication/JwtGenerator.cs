using Microsoft.IdentityModel.Tokens;
using NT.Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NT.Infrastructure.Authentication;

internal class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _settings;

    public JwtGenerator(JwtSettings settings)
    {
        _settings = settings;
    }

    public string GenerateToken(int userId, int roleId)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(IdentityPolicy.RoleClaim, roleId.ToString())
        };

        var signIngCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            claims: claims,
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            expires: DateTime.Now.AddMinutes(_settings.ExpiryInMinutes),
            signingCredentials: signIngCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
