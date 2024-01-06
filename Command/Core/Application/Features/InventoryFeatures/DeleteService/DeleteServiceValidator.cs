using FluentValidation;

namespace Command.Core.Application.Features.InventoryFeatures.DeleteService;

public class DeleteServiceValidator : AbstractValidator<DeleteServiceRequest>
{
    public DeleteServiceValidator()
    {
        RuleFor(x => x.Code)
      .NotNull()
      .GreaterThan(0);
    }
}
