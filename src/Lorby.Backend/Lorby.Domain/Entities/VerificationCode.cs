using Lorby.Domain.Common.Entities;

namespace Lorby.Domain.Entities;

/// <summary>
/// Represents a verification code entity.
/// </summary>
public class VerificationCode : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the verification code.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the expiration time of the verification code.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the verification code is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the actual verification code.
    /// </summary>
    public string Code { get; set; } = default!;
}