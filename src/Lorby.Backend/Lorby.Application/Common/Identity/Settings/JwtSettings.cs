namespace Lorby.Application.Common.Identity.Settings;

/// <summary>
/// Configuration settings for JSON Web Token (JWT) authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used to sign and verify JWTs.
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Gets or sets a flag indicating whether to validate the issuer of the JWT.
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    /// Gets or sets the valid issuer of the JWT (if validation is enabled).
    /// </summary>
    public string ValidIssuer { get; set; } = default!;

    /// <summary>
    /// Gets or sets a flag indicating whether to validate the audience of the JWT.
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    /// Gets or sets the valid audience of the JWT (if validation is enabled).
    /// </summary>
    public string ValidAudience { get; set; } = default!;

    /// <summary>
    /// Gets or sets a flag indicating whether to validate the expiration time of the JWT.
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    /// Gets or sets the expiration time (in minutes) for the JWT.
    /// </summary>
    public int ExpirationTimeInMinutes { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether to validate the issuer signing key of the JWT.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }  
}