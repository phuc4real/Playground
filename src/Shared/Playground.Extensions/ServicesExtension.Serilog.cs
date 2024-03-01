using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Playground.Middleware;
using Serilog;

namespace Playground.Extension;
public static partial class ServicesExtension
{
    public static void ConfigureLogging(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((ctx, cfg) =>
        {
            cfg.ReadFrom.Configuration(ctx.Configuration)
               .Enrich.WithEnvironmentName();
        });
    }

    public static IServiceCollection AddSerilogMiddleware(this IServiceCollection services)
        => services.AddScoped<SerilogMiddleware>();

    public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        app.UseMiddleware<SerilogMiddleware>();
        return app;
    }
}

