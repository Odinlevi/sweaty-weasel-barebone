using MediatR;

namespace Shared.Application;

public interface ITransactionCommand<TResponse> : IRequest<TResponse> where TResponse : notnull
{
}
