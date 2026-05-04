using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.DeleteClient;

public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");
    }
}
