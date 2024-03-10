namespace Lorby.Application.Common.Identity.Settings;

/// <summary>
/// Represents validation settings
/// </summary>
public class ValidationSettings
{
    /// <summary>
    /// Gets or sets Email Regex pattern for validation
    /// </summary>
    public string EmailRegexPattern { get; set; } = default!;

    /// <summary>
    /// Gets or sets Password Regex pattern for validation
    /// </summary>
    public string PasswordRegexPattern { get; set; } = default!;
}