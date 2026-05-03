using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries.GetClientDetailsById;

public class GetClientDetailsByIdRequestValidator : AbstractValidator<GetClientDetailsByIdRequest>
{
    public GetClientDetailsByIdRequestValidator()
    {
        RuleFor(x => x.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");
    }
}
