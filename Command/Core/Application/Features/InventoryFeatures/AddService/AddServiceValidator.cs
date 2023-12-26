using FluentValidation;

namespace Command.Core.Application.Features.InventoryFeatures.AddService;

public class AddServiceValidator : AbstractValidator<AddServiceRequest>
{
    public AddServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.DefaultPrice)
            .NotNull()
            .GreaterThan(0);
    }
}
