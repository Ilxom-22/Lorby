using Lorby.Domain.Entities;
using Lorby.Domain.Enums;
using Lorby.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Api.SeedData;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        
        if (!await appDbContext.EmailTemplates.AnyAsync())
            await appDbContext.SeedEmailTemplates(webHostEnvironment);

        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }
    
    private static async ValueTask SeedEmailTemplates(this AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        var emailTemplateTypes = new List<NotificationTemplateType>
        {
            NotificationTemplateType.EmailVerificationNotification,
        };

        var emailTemplateContents = await Task.WhenAll(
            emailTemplateTypes.Select(
                async templateType =>
                {
                    var filePath = Path.Combine(
                        webHostEnvironment.ContentRootPath,
                        "SeedData",
                        "EmailTemplates",
                        Path.ChangeExtension("EmailVerificationTemplate", "html")
                    );
                    return (TemplateType: templateType, TemplateContent: await File.ReadAllTextAsync(filePath));
                }
            )
        );

        var emailTemplates = emailTemplateContents.Select(
            templateContent => templateContent.TemplateType switch
            {
                NotificationTemplateType.EmailVerificationNotification => new EmailTemplate
                {
                    TemplateType = templateContent.TemplateType,
                    Subject = "Confirm your email address",
                    Content = templateContent.TemplateContent
                },
                _ => throw new NotSupportedException("Template type not supported.")
            }
        );

        await appDbContext.EmailTemplates.AddRangeAsync(emailTemplates);
    }
}