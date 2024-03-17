using System.Reflection;
using System.Text;
using FluentValidation;
using Lorby.Api.Filters;
using Lorby.Api.SeedData;
using Lorby.Application.Common.Identity.Services;
using Lorby.Application.Common.Identity.Settings;
using Lorby.Application.Common.Notification.Brokers;
using Lorby.Application.Common.Notification.Services;
using Lorby.Application.Common.Verifications.Services;
using Lorby.Infrastructure.Common.Identity.Services;
using Lorby.Infrastructure.Common.Notifications.Brokers;
using Lorby.Infrastructure.Common.Notifications.Services;
using Lorby.Infrastructure.Common.Verifications.Services;
using Lorby.Infrastructure.Settings;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories;
using Lorby.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        builder.Services.AddControllers(configs => configs.Filters.Add<ExceptionFilter>());

        return builder;
    }
    
    /// <summary>
    /// Configures AutoMapper for object-to-object mapping using the specified profile.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }
    
    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);
        return builder;
    }

    /// <summary>
    /// Configures database for the project
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Environment.IsDevelopment()
            ? builder.Configuration.GetConnectionString("DbConnectionString")
            : Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DbConnectionString");
        
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));

        return builder;
    }
    
    /// <summary>
    /// Asynchronously migrates database schemas associated with the application.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <returns>A ValueTask representing the asynchronous operation, with the WebApplication instance.</returns>
    private static async ValueTask<WebApplication> MigrateDataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);

        await serviceScopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }
    
    /// <summary>
    /// Seeds data into the application's database by creating a service scope and initializing the seed operation.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    /// <summary>
    /// Configures CORS
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policyBuilder =>
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            )
        );

        return builder;
    }

    /// <summary>
    /// Configures identity related services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // configure settings
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));
        
        // configure jwt settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        
        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
        
        // register helper services
        builder.Services
            .AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddTransient<IAccessTokenGeneratorService, AccessTokenGeneratorService>();
        
        // register repositories
        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>();
        
        // register services
        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>();
        
        return builder;
    }
    
    /// <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // configure SmtpEmailSenderSettings 
        builder.Services.Configure<SmtpEmailSenderSettings>(
            builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)));

        // register repositories
        builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        
        // register brokers
        builder.Services.AddTransient<IEmailSenderBroker, SmtpEmailSenderBroker>();
        
        // register services
        builder.Services
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<IEmailOrchestrationService, EmailOrchestrationService>();
        
        return builder;
    }
    
    private static WebApplicationBuilder AddVerificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register repositories
        builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

        // register services
        builder.Services.AddScoped<IVerificationCodeService, VerificationCodeService>();

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

    /// <summary>
    /// Uses CORS
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseCustomCors(this WebApplication app)
    {
        app.UseCors("CorsPolicy");

        return app;
    }
}