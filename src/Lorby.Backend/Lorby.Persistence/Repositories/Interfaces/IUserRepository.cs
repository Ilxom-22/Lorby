using System.Linq.Expressions;
using Lorby.Domain.Entities;

namespace Lorby.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing user-related data operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves queryable list of users based on the provided predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false);
    
    /// <summary>
    /// Retrieves a user by their unique identifier 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the User object if found, or null if not found.</returns>
    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false,
                                  CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the created User object.</returns>
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the created User object.</returns>
    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true,
                                     CancellationToken cancellationToken = default);
}