namespace Lorby.Application.Common.Notification.Services;

public interface IEmailOrchestrationService
{
    /// <summary>
    /// Sends verification email to the given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<bool> SendVerificationEmail(Guid userId, CancellationToken cancellationToken = default);
}