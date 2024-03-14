using System.Linq.Expressions;
using Lorby.Application.Common.Notification.Services;
using Lorby.Domain.Entities;
using Lorby.Domain.Enums;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Infrastructure.Common.Notifications.Services;

/// <summary>
/// A service that provides functionality for managing email templates.
/// </summary>
public class EmailTemplateService(IEmailTemplateRepository emailTemplateRepository) : IEmailTemplateService
{
    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default,
                                         bool asNoTracking = false)
        => emailTemplateRepository.Get(predicate, asNoTracking);

    public async ValueTask<EmailTemplate?> GetByTypeAsync(NotificationTemplateType templateType,
                                                          bool asNoTracking = false,
                                                          CancellationToken cancellationToken = default)
        => await emailTemplateRepository.Get(emailTemplate => emailTemplate.TemplateType == templateType, asNoTracking)
                                        .SingleOrDefaultAsync(cancellationToken);
}