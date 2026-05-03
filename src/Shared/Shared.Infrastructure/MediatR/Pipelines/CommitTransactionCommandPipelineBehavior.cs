using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application;

namespace Shared.Infrastructure.MediatR.Pipelines;

public class CommitTransactionCommandPipelineBehavior<TRequest, TResponse>(DbContext dbContext)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionCommand<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest                          request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken                 cancellationToken
    )
    {
        var response = await next();

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        );

        return response;
    }
}
