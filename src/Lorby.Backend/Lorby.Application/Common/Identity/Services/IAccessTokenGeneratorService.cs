using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Identity.Services;

/// <summary>
/// Interface for a service responsible for generating and managing access tokens.
/// </summary>
public interface IAccessTokenGeneratorService
{
    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GetToken(User user);

    /// <summary>
    /// Generates a JWT security token for the specified user and access token.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    JwtSecurityToken GetJwtToken(User user);

    /// <summary>
    /// Retrieves a list of claims associated with the specified user and access token.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    List<Claim> GetClaims(User user);
}