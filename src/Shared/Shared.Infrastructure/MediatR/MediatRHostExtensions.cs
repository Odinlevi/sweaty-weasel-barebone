using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.MediatR.Pipelines;

namespace Shared.Infrastructure.MediatR;

public static class MediatRHostExtensions
{
    public static WebApplicationBuilder AddCustomMediatR(
        this   WebApplicationBuilder builder,
        params Assembly[]            assemblies)
    {
        var services = builder.Services;

        services.AddMediatR(mediatR =>
            {
                mediatR
                    .RegisterServicesFromAssemblies(assemblies)
                    .AddOpenBehavior(typeof(ValidateRequestPipelineBehavior<,>))
                    .AddOpenBehavior(typeof(CommitTransactionCommandPipelineBehavior<,>));
            }
        );

        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        services.AddValidatorsFromAssemblies(assemblies);

        return builder;
    }
}
