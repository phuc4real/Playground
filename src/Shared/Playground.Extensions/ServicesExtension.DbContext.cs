using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Playground.Extension;
public static partial class ServicesExtension
{
    public static void ConfigureDbContext<T>(this IServiceCollection services, IConfiguration configuration, string? connectionStringName = null) where T : DbContext
    {
        var conStr = configuration.GetConnectionString(connectionStringName ?? "DefaultConnection");

        services.AddDbContextPool<T>((serviceProvider, options) =>
        {
            options.UseSqlServer(conStr);
        });
    }
}
