using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.OpenApi;
using Modules.ClientRegistry.Infrastructure;
using Modules.ClientRegistry.WebApi.Infrastructures;
using Modules.ClientRegistry.WebApi.Infrastructures.JsonConverters;

var builder  = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Configuration
    .AddJsonFile(path: "appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.AddClientRegistryInfrastructure("DefaultDb");

services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder                     = JavaScriptEncoder.Create(UnicodeRanges.All);
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.AllowTrailingCommas         = true;
        options.JsonSerializerOptions.PropertyNamingPolicy        = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new IdentityJsonConverterFactory());
    }
);

services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.Encoder                     = JavaScriptEncoder.Create(UnicodeRanges.All);
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
        options.SerializerOptions.AllowTrailingCommas         = true;
        options.SerializerOptions.PropertyNamingPolicy        = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.Converters.Add(new IdentityJsonConverterFactory());
    }
);

services.AddOpenApi(options => { options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0; });
services.AddSwaggerConfig();

builder.Host
    .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes  = context.HostingEnvironment.IsDevelopment();
            options.ValidateOnBuild = true;
        }
    );

var app = builder.Build();
var env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerMiddleware();
}

app.UseRouting();
app.MapControllers();

app.MapGet(pattern: "/", handler: () => TypedResults.Redirect(url: "/swagger", permanent: true));

await app.RunAsync();

public partial class Program
{
}
