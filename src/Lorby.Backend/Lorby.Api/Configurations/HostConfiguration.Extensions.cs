using System.Reflection;
using Lorby.Application.Common.Identity;
using Lorby.Application.Common.Notification.Services;
using Lorby.Infrastructure.Common.Identity.Services;
using Lorby.Infrastructure.Notifications.Services;
using Lorby.Infrastructure.Settings;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }
    
    /// <summary>
    /// Configures devTools
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
    
    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    /// <summary>
    /// Configures database for the project
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("LorbyDatabase"));

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // add helper services
        builder.Services.AddTransient<IPasswordHasherService, PasswordHasherService>();

        return builder;
    }
    
    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    
    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
    
    // <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // Configure SmtpEmailSenderSettings 
        builder.Services.Configure<SmtpEmailSenderSettings>(
            builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)));

        // Add EmailTemplateRepository to DI as a Scoped service
        builder.Services
               .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        
        // Add EmailTemplateService to DI as a Scoped service
        builder.Services
               .AddScoped<IEmailTemplateService, EmailTemplateService>();
        
        
        return builder;
    }

}