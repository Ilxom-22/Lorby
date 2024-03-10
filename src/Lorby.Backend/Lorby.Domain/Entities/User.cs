using Lorby.Domain.Common.Entities;

namespace Lorby.Domain.Entities;

/// <summary>
/// represents a user in the system.
/// </summary>

/// <summary>
/// represents a user in the system.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Gets or sets the unique username of the user.
    /// </summary>
    public string UserName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Indicates whether the email address of the user has been verified.
    /// </summary>
    public bool IsEmailAddressVerified { get; set; }
}