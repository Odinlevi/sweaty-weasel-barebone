using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Shared.Infrastructure.MediatR.Pipelines;

public class ValidateRequestPipelineBehavior<TRequest, TResponse>(
    IValidator<TRequest>  validator,
    IOptions<JsonOptions> jsonOptions)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = jsonOptions.Value.JsonSerializerOptions;

    public async Task<TResponse> Handle(
        TRequest                          request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken                 cancellationToken
    )
    {
        var canValidate = validator.CanValidateInstancesOfType(request.GetType());

        if (!canValidate) return await next(cancellationToken);

        var validateResult = await validator.ValidateAsync(instance: request, cancellation: cancellationToken);

        if (validateResult.IsValid) return await next(cancellationToken);

        var errors = validateResult.Errors.Select(_ => _.ErrorMessage).ToList();
        throw new ValidationException(JsonSerializer.Serialize(value: errors, options: _jsonSerializerOptions));
    }
}
