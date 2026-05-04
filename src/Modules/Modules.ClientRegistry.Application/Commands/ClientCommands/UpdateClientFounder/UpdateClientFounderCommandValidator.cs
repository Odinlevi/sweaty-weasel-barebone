using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.UpdateClientFounder;

public class UpdateClientFounderCommandValidator : AbstractValidator<UpdateClientFounderCommand>
{
    public UpdateClientFounderCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");

        RuleFor(c => c.FounderId)
            .NotNull().Must(_ => _ != FounderId.Empty).WithMessage("Founder ID cannot be empty.");

        RuleFor(c => c.FounderFullName)
            .NotEmpty().WithMessage("Founder Name cannot be empty.")
            .MaximumLength(200).WithMessage("Founder Name cannot exceed 200 characters.");
    }
}
