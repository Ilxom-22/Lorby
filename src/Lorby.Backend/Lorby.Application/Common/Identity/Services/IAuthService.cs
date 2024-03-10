using Lorby.Application.Common.Identity.Models;

namespace Lorby.Application.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration and login.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Asynchronously registers a new user with the specified registration details.
    /// </summary>
    /// <param name="signUpDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously attempts to log in a user with the provided credentials.
    /// </summary>
    /// <param name="signInDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public ValueTask<string> SignInAsync(SignInDetails signInDetails, 
        CancellationToken cancellationToken = default);
}
