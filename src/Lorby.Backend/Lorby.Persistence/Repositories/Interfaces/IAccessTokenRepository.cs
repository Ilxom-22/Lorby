using Lorby.Domain.Entities;

namespace Lorby.Persistence.Repositories.Interfaces;

public interface IAccessTokenRepository
{
    /// <summary>
    /// Retrieves access token based on the provided token
    /// </summary>
    /// <param name="token"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<AccessToken?> GetByToken(string token, bool asNoTracking = false,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously creates a new AccessToken entity.
    /// </summary>
    /// <param name="accessToken">The AccessToken entity to be created.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the created AccessToken.</returns>
    ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an AccessToken entity by its unique identifier.
    /// </summary>
    /// <param name="accessTokenId">The unique identifier of the AccessToken entity to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the retrieved AccessToken, or null if not found.</returns>
    ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing AccessToken entity.
    /// </summary>
    /// <param name="accessToken">The AccessToken entity to be updated.</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none).</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the updated AccessToken.</returns>
    ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes access token by Id
    /// </summary>
    /// <param name="accessTokenId">Id of access token to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask<AccessToken?> DeleteByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);
}