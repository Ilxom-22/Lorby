namespace Lorby.Application.Common.Identity.Models;

/// <summary>
/// Represents sign up details for authorization
/// </summary>
public class SignUpDetails
{
    /// <summary>
    /// Gets or sets the username of the user
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password of the user
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password confirmation of the user
    /// </summary>
    public string PasswordConfirmation { get; set; } = default!;
}