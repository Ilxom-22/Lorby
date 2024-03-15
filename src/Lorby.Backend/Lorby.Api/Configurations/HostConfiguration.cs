namespace Lorby.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddCustomCors()
            .AddDevTools()
            .AddMappers()
            .AddValidators()
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddVerificationInfrastructure()
            .AddNotificationInfrastructure()
            .AddExposers();

        return new(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.MigrateDataBaseSchemasAsync();
        await app.SeedDataAsync();
        
        app
            .UseCustomCors()
            .UseDevTools()
            .UseExposers();
        
        return app;
    }
}