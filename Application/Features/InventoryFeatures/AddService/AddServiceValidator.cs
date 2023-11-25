using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
    public class AddServiceValidator : AbstractValidator<AddServiceRequest>
    {
        public AddServiceValidator()
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
