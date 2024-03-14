using System.Security.Cryptography;
using Lorby.Application.Common.Verifications.Services;
using Lorby.Domain.Entities;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Infrastructure.Common.Verifications.Services;

/// <summary>
/// Implements methods for managing verification codes using a specified repository.
/// </summary>
public class VerificationCodeService(IVerificationCodeRepository verificationCodeRepository) : IVerificationCodeService
{
    public string Generate()
    {
        using var rng = RandomNumberGenerator.Create();
        return Enumerable.Range(0, 4)
                         .Select(_ =>
                             {
                                 var randomNumber = new byte[1];
                                 rng.GetBytes(randomNumber);
                                 return (randomNumber[0] % 10).ToString();
                             }
                         )
                         .Aggregate((code, digit) => code + digit);
    }

    public async ValueTask<(VerificationCode Code, bool IsValid)> GetByCodeAsync(
        string code, CancellationToken cancellationToken = default)
    {
        var verificationCode = await verificationCodeRepository
                                     .Get(verificationCode => verificationCode.Code == code, true)
                                     .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException();

        return (verificationCode, verificationCode.IsActive && verificationCode.ExpiryTime > DateTimeOffset.UtcNow);
    }

    public async ValueTask<VerificationCode> CreateAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var oldCode = await GetByUserId(userId, cancellationToken);
        
        if (oldCode is not null)
            await DeactivateAsync(oldCode.Id, cancellationToken: cancellationToken);
        
        var verificationCode = new VerificationCode()
        {
            UserId = userId,
            Code = Generate(),
            ExpiryTime = DateTimeOffset.UtcNow.AddMinutes(2),
            IsActive = true
        };

        await verificationCodeRepository.CreateAsync(verificationCode, cancellationToken: cancellationToken);

        return verificationCode;
    }

    public ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true,
                                     CancellationToken cancellationToken = default)
    {
        return verificationCodeRepository.DeactivateAsync(codeId, saveChanges, cancellationToken);
    }

    private async ValueTask<VerificationCode?> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await verificationCodeRepository
            .Get(code => code.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}