using Command.Core.Application.Features.InventoryFeatures.AddService;
using FluentValidation;

namespace Command.Core.Application.Features.InventoryFeatures.UpdateService
{
    public class UpdateServiceValidator : AbstractValidator<AddServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.DefaultPrice)
                .NotNull()
            .GreaterThan(0);
        }
    }
}
