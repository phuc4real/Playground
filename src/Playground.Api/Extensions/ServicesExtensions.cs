using Serilog;

namespace Playground.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => 
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(opt =>
            {
            });

        public static void ConfigureSerilog(HostBuilderContext context, ILoggingBuilder logging)
        {
            var appsettings = context.Configuration;

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(appsettings)
               .Enrich.FromLogContext()
               .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
               .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
               .CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog();
        }
    }
}
