namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddCarter();
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandler(options => { });

        app.MapCarter();

        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
