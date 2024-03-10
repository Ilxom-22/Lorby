using System.Linq.Expressions;
using Lorby.Application.Common.Identity.Services;
using Lorby.Domain.Entities;
using Lorby.Persistence.Repositories.Interfaces;

namespace Lorby.Infrastructure.Common.Identity.Services;

/// <summary>
/// Represents a service for managing users.
/// </summary>
public class UserService(IUserRepository userRepository) : IUserService
{
    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false)
        => userRepository.Get(predicate, asNoTracking);

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false,
                                         CancellationToken cancellationToken = default)
        => userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public ValueTask<User> CreateAsync(User user, bool saveChanges = true,
                                       CancellationToken cancellationToken = default)
        => userRepository.CreateAsync(user, saveChanges, cancellationToken);
}