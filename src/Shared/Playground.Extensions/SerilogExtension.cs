using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Context;

namespace Playground.Extension;
public static class SerilogExtension
{
    #region Configure Logging

    public static void ConfigureLogging(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((ctx, cfg) =>
        {
            cfg.ReadFrom.Configuration(ctx.Configuration)
               .Enrich.WithEnvironmentName();
        });
    }

    #endregion

    #region Serilog Middleware

    public class SerilogMiddleware() : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            context.Response.Headers["X-Correlation-ID"] = correlationId;

            using (LogContext.PushProperty("CorrelationID", correlationId))
            {
                await next(context);
            }
        }
    }

    public static IServiceCollection AddSerilogMiddleware(this IServiceCollection services)
        => services.AddScoped<SerilogMiddleware>();

    public static IApplicationBuilder UseSerilogMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<SerilogMiddleware>();


    #endregion
}
