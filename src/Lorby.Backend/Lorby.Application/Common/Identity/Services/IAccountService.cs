using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Identity.Services;

/// <summary>
/// Represents a service for managing user accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Retrieves a user by their username
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> GetUserByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Creates a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies User email address using verification code
    /// </summary>
    /// <param name="verificationCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> VerificateAsync(string verificationCode, CancellationToken cancellationToken = default);
}