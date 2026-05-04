using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Modules.ClientRegistry.Application.Commands.Exceptions;

namespace Modules.ClientRegistry.WebApi.Infrastructures.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, IOptions<JsonOptions> jsonOptionsAccessor)
{
    private readonly JsonOptions _jsonOptions = jsonOptionsAccessor.Value;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context: httpContext, exception: ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = new ExceptionResponse();

        context.Response.ContentType    = "application/json";
        context.Response.StatusCode     = (int)HttpStatusCode.InternalServerError;
        exceptionResponse.ErrorMessages = [exception.Message];

        switch (exception)
        {
            case ValidationException validationException:
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                exceptionResponse.ErrorMessages = ParseOrWrapMessage(validationException.Message);
                break;
            }
            case NotFoundEntityException notFoundEntityException:
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                exceptionResponse.ErrorMessages = ParseOrWrapMessage(notFoundEntityException.Message);
                break;
            }
        }

        exceptionResponse.Status = context.Response.StatusCode;

        var jsonContent = JsonSerializer.Serialize(value: exceptionResponse, options: _jsonOptions.SerializerOptions);

        await context.Response.WriteAsync(jsonContent);
    }

    private List<string> ParseOrWrapMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return [];

        try
        {
            var parsed = JsonSerializer.Deserialize<List<string>>(
                json: message, options: _jsonOptions.SerializerOptions
            );

            if (parsed is { Count: > 0 })
                return parsed;
        }
        catch (JsonException)
        {
            // Message is plain text, not JSON.
        }

        return [message];
    }

    public class ExceptionResponse
    {
        public int          Status        { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}
