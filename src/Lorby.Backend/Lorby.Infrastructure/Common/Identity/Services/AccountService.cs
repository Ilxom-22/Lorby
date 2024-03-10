using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Notification.Services;
using Lorby.Application.Common.Verifications.Services;
using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class AccountService(
    IUserService userService,
    IEmailOrchestrationService emailOrchestrationService,
    IVerificationCodeService verificationCodeService) 
    : IAccountService
{
    public async ValueTask<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await userService
            .Get(user => user.UserName == username)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask<User?> GetUserByEmailAddressAsync(string emailAddress,
        CancellationToken cancellationToken = default)
    {
        return await userService
            .Get(user => user.EmailAddress == emailAddress, true)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);

        await emailOrchestrationService.SendVerificationEmail(user.Id, cancellationToken);

        return createdUser;
    }

    public async ValueTask<bool> VerifyAsync(Guid userId, string verificationCode, CancellationToken cancellationToken = default)
    {
        var foundVerificationCode = await verificationCodeService.GetByCodeAsync(verificationCode, cancellationToken);

        if (!foundVerificationCode.IsValid || foundVerificationCode.Code.UserId != userId)
            throw new InvalidOperationException("Invalid verification code");

        var foundUser = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
                        ?? throw new ArgumentException($"User with id {userId} not found!");

        foundUser.IsEmailAddressVerified = true;

        await userService.UpdateAsync(foundUser, cancellationToken: cancellationToken);

        return true;
    }
}