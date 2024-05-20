using Identity.Model;
using Identity.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Playground.Extension;

var builder = WebApplication.CreateBuilder(args);
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
builder.Services.ConfigureCors();

builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

builder.Services.ConfigureDbContext<IdentityRepositoryContext>(configuration);

builder.Services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<IdentityRepositoryContext>()
                .AddApiEndpoints();

var app = builder.Build();

app.MapGroup("identity")
   .WithTags("Identity")
   .MapIdentityApi<AppUser>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogMiddleware();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
