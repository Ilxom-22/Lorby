using System.Linq.Expressions;
using Lorby.Domain.Entities;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Persistence.Repositories;

/// <summary>
/// Implements methods for managing user information verification codes within the specified database context.
/// </summary>
public class VerificationCodeRepository(AppDbContext appDbContext) : EntityRepositoryBase<VerificationCode, AppDbContext>(appDbContext), IVerificationCodeRepository
{
    /// <inheritdoc/>
    public new IQueryable<VerificationCode> Get(Expression<Func<VerificationCode, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    /// <inheritdoc/>
    public new ValueTask<VerificationCode?> GetByIdAsync(Guid codeId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(codeId, asNoTracking, cancellationToken);

    /// <inheritdoc/>
    public new ValueTask<VerificationCode> CreateAsync(VerificationCode verificationCode, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(verificationCode, saveChanges, cancellationToken);

    /// <inheritdoc/>
    public async ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundVerificationCode = await DbContext.Verifications.FirstOrDefaultAsync(code => code.Id == codeId, cancellationToken)
                                    ?? throw new FileNotFoundException(nameof(codeId));

        foundVerificationCode.IsActive = false;

        await UpdateAsync(foundVerificationCode, cancellationToken: cancellationToken);
    }
}
