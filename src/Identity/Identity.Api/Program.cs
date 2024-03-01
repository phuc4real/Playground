using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Playground.Extension;
using Identity.Repository;
using Identity.Model;

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
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
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

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
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
