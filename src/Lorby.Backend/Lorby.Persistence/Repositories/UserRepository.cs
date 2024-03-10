using System.Linq.Expressions;
using Lorby.Domain.Entities;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories.Interfaces;

namespace Lorby.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing user entities
/// </summary>
public class UserRepository(AppDbContext dbContext)
    : EntityRepositoryBase<User, AppDbContext>(dbContext), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
    
    public new ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false,
                                             CancellationToken cancellationToken = default)
        => base.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public new ValueTask<User> CreateAsync(User user, bool saveChanges = true,
                                           CancellationToken cancellationToken = default)
        => base.CreateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true,
                                           CancellationToken cancellationToken = default)
        => base.UpdateAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User?> DeleteAsync(User user, bool saveChanges = true,
                                            CancellationToken cancellationToken = default)
        => base.DeleteAsync(user, saveChanges, cancellationToken);

    public new ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true,
                                                CancellationToken cancellationToken = default)
        => base.GetByIdAsync(userId, saveChanges, cancellationToken);
}