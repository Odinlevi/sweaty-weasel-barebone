using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");

        RuleFor(x => x.ClientName)
            .NotEmpty().WithMessage("Client name is required.")
            .MaximumLength(200).WithMessage("Client name cannot exceed 200 characters.");
    }
}
