using Identity.Model;
using Identity.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Playground.Extension;

namespace Identity.Api.Extensions;

public static class HostBuilderExtensions
{
    public static void HostBuilderConfiguration(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Host.ConfigureLogging();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Playground Identity API",
                Description = "Identity Module",
                Contact = new OpenApiContact
                {
                    Name = "Github: phuc4real",
                    Url = new Uri("https://github.com/phuc4real")
                }
            });
        });
        builder.Services.AddSerilogMiddleware();
        builder.Services.AddCustomCors();
        builder.Services.AddAuthentication()
                        .AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddAuthorizationBuilder();
        builder.Services.ConfigureDbContext<IdentityRepositoryContext>(configuration);
                builder.Services.AddIdentityCore<AppUser>()
                        .AddEntityFrameworkStores<IdentityRepositoryContext>()
                        .AddApiEndpoints();
    }
}
