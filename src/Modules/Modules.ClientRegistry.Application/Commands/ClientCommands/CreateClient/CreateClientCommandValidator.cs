using FluentValidation;
using Modules.ClientRegistry.Domain.ClientTypes;

namespace Modules.ClientRegistry.Application.Commands.ClientCommands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Client name is required.")
            .MaximumLength(200).WithMessage("Client name cannot exceed 200 characters.");

        RuleFor(x => x.Inn)
            .NotEmpty().WithMessage("Client INN is required.")
            .MaximumLength(12).WithMessage("Client INN cannot exceed 12 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("A valid Client Type must be provided.");

        RuleFor(x => x.Founders)
            .Empty()
            .When(x => x.Type == ClientType.IndividualEntrepreneur)
            .WithMessage("An Individual Entrepreneur cannot have founders.");

        RuleFor(x => x.Founders)
            .NotEmpty()
            .When(x => x.Type == ClientType.LegalEntity)
            .WithMessage("A Legal Entity must have at least one founder.");

        RuleFor(x => x.Founders)
            .Must(founders => founders.Count <= 10)
            .WithMessage("A client cannot have more than 10 founders.");

        When(
            predicate: x => x.Founders.Count != 0, action: () =>
            {
                RuleForEach(x => x.Founders).ChildRules(founder =>
                    {
                        founder.RuleFor(f => f.FullName)
                            .NotEmpty().WithMessage("Founder full name is required.")
                            .MaximumLength(200).WithMessage("Founder full name cannot exceed 200 characters.");

                        founder.RuleFor(f => f.Inn)
                            .NotEmpty().WithMessage("Founder INN is required.")
                            .MaximumLength(12).WithMessage("Founder INN must be cannot exceed 12 characters.");
                    }
                );

                RuleFor(x => x.Founders)
                    .Must(founders => founders.Select(f => f.Inn).Distinct().Count() == founders.Count)
                    .WithMessage("A client cannot be created with multiple founders sharing the same INN.");
            }
        );
    }
}
