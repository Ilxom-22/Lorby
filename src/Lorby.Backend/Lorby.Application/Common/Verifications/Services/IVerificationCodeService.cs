using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Verifications.Services;

/// <summary>
/// Defines methods for managing user information verification codes.
/// </summary>
public interface IVerificationCodeService
{
    /// <summary>
    /// Generates a list of user information verification codes.
    /// </summary>
    /// <returns>A list of generated codes.</returns>
    string Generate();

    /// <summary>
    /// Retrieves a user information verification code by its code asynchronously.
    /// </summary>
    /// <param name="code">The code to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A tuple containing the verification code and a boolean indicating its validity.</returns>
    ValueTask<(VerificationCode Code, bool IsValid)> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user information verification code asynchronously.
    /// </summary>
    /// <param name="codeType">The type of verification code to create.</param>
    /// <param name="userId">The unique identifier of the user associated with the verification code.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result is the created user information verification code.</returns>
    ValueTask<VerificationCode> CreateAsync(VerificationCode codeType, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates user information verification codes associated with the specified user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose codes should be deactivated.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the underlying storage.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask DeactivateAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);
}
