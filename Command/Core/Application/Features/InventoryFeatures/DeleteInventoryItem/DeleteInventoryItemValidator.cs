using FluentValidation;
using OctaShared.DTOs.Request;

namespace Command.Core.Application.Features.InventoryFeatures.AddService;

public class DeleteInventoryItemValidator : AbstractValidator<DeleteInventoryItemRequest>
{
    public DeleteInventoryItemValidator()
    {
        RuleFor(x => x.Code)
      .NotNull()
      .NotEmpty()
      .GreaterThan(0);
    }
}
