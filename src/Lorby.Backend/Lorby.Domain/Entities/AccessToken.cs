using Lorby.Domain.Common.Entities;

namespace Lorby.Domain.Entities;

public class AccessToken : Entity
{
    /// <summary>
    /// Gets or sets the access token string.
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier of the associated user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the access token is revoked.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Represents User navigation property
    /// </summary>
    public User User { get; set; }
}