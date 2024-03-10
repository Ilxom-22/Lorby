using Lorby.Domain.Common.Entities;

namespace Lorby.Domain.Entities;

/// <summary>
/// represents a user in the system.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Indicates whether the email address of the user has been verified.
    /// </summary>
    public bool IsEmailVerified { get; set; }
}