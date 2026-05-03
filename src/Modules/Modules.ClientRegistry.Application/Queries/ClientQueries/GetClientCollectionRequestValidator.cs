using FluentValidation;

namespace Modules.ClientRegistry.Application.Queries.ClientQueries;

public class GetClientCollectionRequestValidator : AbstractValidator<GetClientCollectionRequest>
{
    public GetClientCollectionRequestValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
    }
}
