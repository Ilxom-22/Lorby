using Lorby.Application.Common.Identity.Services;
using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class AccountService(IUserService userService) : IAccountService
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
        
        // TODO: send email

        return createdUser;
    }

    public ValueTask<bool> VerificateAsync(string verificationCode, CancellationToken cancellationToken = default)
    {
        // TODO: send verification email
        throw new NotImplementedException();
    }
}