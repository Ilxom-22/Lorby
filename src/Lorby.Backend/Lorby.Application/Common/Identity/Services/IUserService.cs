using System.Linq.Expressions;
using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Identity.Services;

/// <summary>
///     Provides operations for managing users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a collection of users that match the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the users.</param>
    /// <param name="asNoTracking">A value indicating whether to disable change tracking.</param>
    /// <returns>A collection of users.</returns>
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Gets a user that matches the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="asNoTracking">A value indicating whether to disable change tracking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user.</returns>
    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="saveChanges">A value indicating whether to save changes.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created user.</returns>
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);
}