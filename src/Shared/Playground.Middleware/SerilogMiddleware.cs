using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Playground.Middleware;

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