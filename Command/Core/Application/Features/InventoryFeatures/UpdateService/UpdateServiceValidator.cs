using FluentValidation;

namespace Command.Core.Application.Features.InventoryFeatures.UpdateService
{
    public class UpdateServiceValidator : AbstractValidator<OctaShared.DTOs.Request.AddServiceRequest>
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
