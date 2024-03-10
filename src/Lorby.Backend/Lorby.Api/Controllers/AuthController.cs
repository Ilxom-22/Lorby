using Lorby.Application.Common.Identity.Models;
using Lorby.Application.Common.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lorby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> SignUpAsync([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        await authService.SignUpAsync(signUpDetails, cancellationToken);
        return NoContent();
    }
}