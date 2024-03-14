using AutoMapper;
using Lorby.Application.Common.Identity.Models;
using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Notification.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lorby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService,
    IAccountService accountService,
    IEmailOrchestrationService emailOrchestrationService,
    IMapper mapper) : ControllerBase
{
    [HttpPost("SignUp")]
    public async ValueTask<IActionResult> SignUpAsync([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var user = await authService.SignUpAsync(signUpDetails, cancellationToken);
        return Ok(mapper.Map<UserDto>(user));
    }

    [HttpPost("SignIn")]
    public async ValueTask<IActionResult> SignInAsync([FromBody] SignInDetails signInDetails,
        CancellationToken cancellationToken = default)
    {
        return Ok(await authService.SignInAsync(signInDetails, cancellationToken));
    }

    [HttpPost("LogOut/{token}")]
    public async ValueTask<IActionResult> LogOutAsync([FromRoute] string token, CancellationToken cancellationToken = default)
    {
        return Ok(await authService.LogOutAsync(token, cancellationToken: cancellationToken));
    }

    [HttpPost("verify/{userId:guid}/{code}")]
    public async ValueTask<IActionResult> VerifyAsync([FromRoute] Guid userId, [FromRoute] string code, CancellationToken cancellationToken = default)
    {
        return Ok(await accountService.VerifyAsync(userId, code, cancellationToken));
    }

    [HttpPost("resendEmail/{userId:guid}")]
    public async ValueTask<IActionResult> ResendEmailVerificationCode([FromRoute] Guid userId, CancellationToken cancellationToken = default!)
    {
        return Ok(await emailOrchestrationService.SendVerificationEmail(userId, cancellationToken));
    }
}