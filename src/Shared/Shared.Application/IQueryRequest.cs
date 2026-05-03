using MediatR;

namespace Shared.Application;

public interface IQueryRequest<TResponse> : IRequest<TResponse> where TResponse : notnull
{
}
