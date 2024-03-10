using Lorby.Domain.Entities;

namespace Lorby.Application.Common.Notification.Models;

/// <summary>
/// Represents an email message for notifications
/// </summary>
public class EmailMessage
{
    /// <summary>
    /// Gets or sets sender user's id of the notification message
    /// </summary>
    public Guid SenderUserId { get; set; }

    /// <summary>
    /// Gets or sets receiver user's id of the notification message
    /// </summary>
    public Guid ReceiverUserId { get; set; }
    
    /// <summary>
    /// Gets or sets email address of sender user
    /// </summary>
    public string SenderEmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets email address of receiver user
    /// </summary>
    public string ReceiverEmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets email template of the email message
    /// </summary>
    public EmailTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets subject of the email message
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Gets or sets body of the email message
    /// </summary>
    public string Body { get; set; } = default!;

    /// <summary>
    /// Gets or sets variables of the notification message
    /// </summary>
    /// <remarks>
    /// These variables is needed for rendering message
    /// </remarks>
    public Dictionary<string, string> Variables { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets if the notification message is send successfully or not
    /// </summary>
    public bool IsSuccessful { get; set; }
}