using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Notification.Services;
using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class AccountService(
    IUserService userService,
    IEmailOrchestrationService emailOrchestrationService) 
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

    public ValueTask<bool> VerifyAsync(string verificationCode, CancellationToken cancellationToken = default)
    {
        // TODO: send verification email
        throw new NotImplementedException();
    }
}