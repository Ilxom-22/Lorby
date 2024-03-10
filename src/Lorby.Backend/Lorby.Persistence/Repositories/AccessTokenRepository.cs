using Lorby.Domain.Entities;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Persistence.Repositories;

public class AccessTokenRepository(AppDbContext dbContext) : EntityRepositoryBase<AccessToken, AppDbContext>(dbContext), IAccessTokenRepository
{
    public async ValueTask<AccessToken?> GetByToken(string token, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await base
            .Get(access => access.Token == token)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public new ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(accessToken, saveChanges, cancellationToken);
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(accessTokenId, cancellationToken: cancellationToken);
    }

    public ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(accessToken, cancellationToken: cancellationToken);
    }

    public ValueTask<AccessToken?> DeleteByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(accessTokenId, cancellationToken: cancellationToken);
    }
}