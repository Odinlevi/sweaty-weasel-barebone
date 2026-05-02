using Modules.ClientRegistry.Infrastructure;

var builder  = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Configuration
    .AddJsonFile(path: "appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.AddClientRegistryInfrastructure("DefaultDb");

builder.Host
    .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes  = context.HostingEnvironment.IsDevelopment();
            options.ValidateOnBuild = true;
        }
    );

var app = builder.Build();
var env = builder.Environment;

if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

app.MapGet(pattern: "/", handler: () => "Hello World!");

await app.RunAsync();

public partial class Program
{
}
