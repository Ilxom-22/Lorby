using System.Linq.Expressions;
using Lorby.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Persistence.Repositories;

public abstract class EntityRepositoryBase<TEntity, TContext> where TEntity : class, IEntity where TContext : DbContext
{
    private readonly DbContext _dbContext;
    protected TContext DbContext => (TContext)_dbContext;

    protected EntityRepositoryBase(DbContext dbContext) =>
        _dbContext = dbContext;

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    public async ValueTask<TEntity?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            return await initialQuery
                .AsNoTracking()
                .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return await initialQuery.FirstAsync(entity => entity.Id == id, cancellationToken);
    }

    public async ValueTask<IList<TEntity>> GetAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        initialQuery = initialQuery.Where(entity => ids.Contains(entity.Id));

        return await initialQuery.ToListAsync(cancellationToken);
    }

    public async ValueTask<TEntity> CreateAsync(
        TEntity entity, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken); 
        
        return entity;
    }

    public async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await GetByIdAsync(id, cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

        return await DeleteAsync(entity, saveChanges, cancellationToken);
    }
}