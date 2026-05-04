using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientFounderCommands.RemoveClientFounder;

public class RemoveClientFounderCommandValidator : AbstractValidator<RemoveClientFounderCommand>
{
    public RemoveClientFounderCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");

        RuleFor(c => c.FounderId)
            .NotNull().Must(_ => _ != FounderId.Empty).WithMessage("Founder ID cannot be empty.");
    }
}
