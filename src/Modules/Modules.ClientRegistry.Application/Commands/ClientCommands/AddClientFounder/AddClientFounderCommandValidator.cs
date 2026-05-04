using FluentValidation;
using Modules.ClientRegistry.Domain.Clients;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.AddClientFounder;

public class AddClientFounderCommandValidator : AbstractValidator<AddClientFounderCommand>
{
    public AddClientFounderCommandValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull().Must(_ => _ != ClientId.Empty).WithMessage("Client ID cannot be empty.");

        RuleFor(x => x.FounderFullName)
            .NotEmpty().WithMessage("Founder full name is required.")
            .MaximumLength(200).WithMessage("Founder full name cannot exceed 200 characters.");

        RuleFor(x => x.FounderInn)
            .NotEmpty().WithMessage("Founder INN is required.")
            .MaximumLength(12).WithMessage("Founder INN cannot exceed 12 characters.");
    }
}
