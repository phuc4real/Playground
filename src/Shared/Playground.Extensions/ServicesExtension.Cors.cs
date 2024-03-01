using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Playground.Extension;
public static partial class ServicesExtension
{
    public static void ConfigureCors(this IServiceCollection services)
    => services.AddCors(options =>
    {
        options.AddPolicy("DefaultCors", builder
            => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
    });

    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedHosts = configuration.GetSection("AllowedHosts").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("CustomCors", builder
                => builder.WithOrigins(allowedHosts)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
        });
    }
}
