using FluentValidation;
namespace Command.Core.Application.Features.InventoryFeatures.AddInventoryItem;
public class AddInventoryItemValidator : AbstractValidator<AddInventoryItemRequest>
{
    public AddInventoryItemValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);
    }
}
