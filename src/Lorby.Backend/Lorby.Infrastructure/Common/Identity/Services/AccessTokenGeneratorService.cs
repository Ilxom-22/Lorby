using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Identity.Settings;
using Lorby.Domain.Constants;
using Lorby.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class AccessTokenGeneratorService : IAccessTokenGeneratorService
{
    private readonly JwtSettings _jwtSettings;

    public AccessTokenGeneratorService(IOptions<JwtSettings> jwtSettings) =>
        _jwtSettings = jwtSettings.Value;
    
    public string GetToken(User user)
    {
        var jwtToken = GetJwtToken(user);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return token;
    }

    public JwtSecurityToken GetJwtToken(User user)
    {
        var claims = GetClaims(user);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credentials);

        return token;
    }

    public List<Claim> GetClaims(User user) =>
    [
        new Claim(ClaimTypes.Email, user.EmailAddress),
        new Claim(ClaimConstants.UserId, user.Id.ToString())
    ];

}