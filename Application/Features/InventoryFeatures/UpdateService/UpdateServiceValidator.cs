using FluentValidation;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateService
{
    public class UpdateServiceValidator : AbstractValidator<AddServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.Name).
                NotEmpty().
                MaximumLength(255);
            RuleFor(x => x.DefaultPrice)
                .NotNull()
            .GreaterThan(0);
        }
    }
}
