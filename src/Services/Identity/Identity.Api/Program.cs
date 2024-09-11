using Identity.Api.Extensions;
using Identity.Model;
using Playground.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.HostBuilderConfiguration();

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