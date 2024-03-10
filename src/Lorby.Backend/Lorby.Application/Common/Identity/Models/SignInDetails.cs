namespace Lorby.Application.Common.Identity.Models;

/// <summary>
/// Gets or sets email address of the login details
/// </summary>
public class SignInDetails
{
    /// <summary>
    /// Gets or sets the username of the user
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password of the user
    /// </summary>
    public string Password { get; set; } = default!;
}