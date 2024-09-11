using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Playground.Extension;
public static partial class ServicesExtension
{
    public static void AddCustomCors(this IServiceCollection services)
    => services.AddCors(options =>
    {
        options.AddPolicy("DefaultCors", builder
            => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
    });

    public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedHosts = configuration.GetSection("AllowedHosts").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("PlaygroundCors", builder
                => builder.WithOrigins(allowedHosts)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
        });
    }

    public static void UseCustomCors(this IApplicationBuilder app)
    {

    }
}
