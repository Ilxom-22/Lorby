using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Notification.Brokers;
using Lorby.Application.Common.Notification.Models;
using Lorby.Application.Common.Notification.Services;
using Lorby.Application.Common.Verifications.Services;
using Lorby.Domain.Constants;
using Lorby.Domain.Entities;
using Lorby.Domain.Enums;

namespace Lorby.Infrastructure.Common.Notifications.Services;

public class EmailOrchestrationService(
    IEmailTemplateService emailTemplateService,
    IVerificationCodeService verificationCodeService,
    IEmailSenderBroker emailSenderBroker,
    IUserService userService)
    : IEmailOrchestrationService
{
    public async ValueTask<bool> SendVerificationEmail(Guid userId, CancellationToken cancellationToken = default)
    {
        var foundUser = await GetUser(userId, cancellationToken);

        var verificationCode = await verificationCodeService.CreateAsync(foundUser.Id, cancellationToken);
        
        var emailTemplate = await emailTemplateService.GetByTypeAsync(NotificationTemplateType.EmailVerificationNotification, true, cancellationToken) 
                            ?? throw new InvalidOperationException();

        var emailMessage = CreateEmailMessage(foundUser, emailTemplate, verificationCode);

        emailMessage.IsSuccessful = await emailSenderBroker.SendAsync(emailMessage, cancellationToken);

        return emailMessage.IsSuccessful;
    }

    private EmailMessage CreateEmailMessage(User user, EmailTemplate emailTemplate, VerificationCode verificationCode)
    {
        var emailMessage = new EmailMessage
        {
            ReceiverUserId = user.Id,
            ReceiverEmailAddress = user.EmailAddress,
            Subject = emailTemplate.Subject,
            Body = emailTemplate.Content
                .Replace(EmailConstants.UserNameToken, user.UserName)
                .Replace(EmailConstants.EmailVerificationCodeToken, verificationCode.Code)
        };

        return emailMessage;
    }

    private async ValueTask<User> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        var foundUser = await userService.GetByIdAsync(userId, true, cancellationToken)
                        ?? throw new ArgumentException($"User with the given id {userId} does not exist.");

        if (foundUser.IsEmailAddressVerified)
            throw new ArgumentException($"Email address of the user with id {userId} is already verified.");

        return foundUser;
    }
}