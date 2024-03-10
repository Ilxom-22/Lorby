using Lorby.Domain.Common.Entities;
using Lorby.Domain.Enums;

namespace Lorby.Domain.Entities;

public class EmailTemplate : Entity
{
    /// <summary>
    /// Gets or sets the subject of the email template.
    /// </summary>
    /// <remarks>
    /// The subject is the main content or description of the email message.
    /// </remarks>
    public string Subject { get; set; } = default!;
    /// <summary>
    /// Gets or sets the content of the notification template.
    /// </summary>
    /// <remarks>
    /// The content is the main body or message of the notification.
    /// </remarks>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the template type of the notification template.
    /// </summary>
    /// <remarks>
    /// The template type specifies the specific subtype or style of the notification template.
    /// </remarks>
    public NotificationTemplateType TemplateType { get; set; }
}