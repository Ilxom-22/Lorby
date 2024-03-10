using System.Security.Authentication;
using AutoMapper;
using FluentValidation;
using Lorby.Application.Common.Identity;
using Lorby.Application.Common.Identity.Models;
using Lorby.Application.Common.Identity.Services;
using Lorby.Domain.Entities;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class AuthService(
    IAccountService accountService, 
    IMapper mapper,
    IPasswordHasherService passwordHasherService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IValidator<SignUpDetails> signUpDetailsValidator) 
    : IAuthService
{
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        await ValidateUserExistence(signUpDetails, cancellationToken);

        var validationResult = await signUpDetailsValidator.ValidateAsync(signUpDetails, cancellationToken);

        if (!validationResult.IsValid)
            throw new ArgumentException("Invalid Sign Up Details!");

        if (signUpDetails.Password != signUpDetails.PasswordConfirmation)
            throw new ArgumentException("Confirmed password is incorrect!");

        var user = mapper.Map<User>(signUpDetails);
        
        user.PasswordHash = passwordHasherService.HashPassword(signUpDetails.Password);
        
        await accountService.CreateUserAsync(user, cancellationToken);

        return true;
    }

    public async ValueTask<string> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = await accountService
            .GetByUsernameAsync(signInDetails.UserName, cancellationToken);
        
        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Sign in details are invalid, contact support.");

        return accessTokenGeneratorService.GetToken(foundUser);
    }

    private async ValueTask ValidateUserExistence(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var foundUserByUserName = await accountService.GetByUsernameAsync(signUpDetails.UserName, cancellationToken);

        var foundUserByEmail =
            await accountService.GetUserByEmailAddressAsync(signUpDetails.EmailAddress, cancellationToken);

        if (foundUserByUserName is not null)
            throw new InvalidOperationException("This username is already taken");
        
        if (foundUserByEmail is not null)
            throw new InvalidOperationException("User with this email address already exists.");
    }
}