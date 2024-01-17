using FluentValidation;
using OctaShared.DTOs.Request;
namespace Command.Core.Application.Features.InventoryFeatures.UpdateInventoryItem;
public class UpdateInventoryItemValidator : AbstractValidator<UpdateInventoryItemRequest>
{
    public UpdateInventoryItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.SellPrice)
            .NotNull()
        .GreaterThanOrEqualTo(0);
        RuleFor(x => x.BuyPrice)
   .NotNull()
.GreaterThanOrEqualTo(0);

        RuleFor(x => x.CountLowerBound)
   .NotNull();
        //.GreaterThanOrEqualTo(0);
        RuleFor(x => x.Count)
.NotNull();
        //.GreaterThanOrEqualTo(0);

    }
}
