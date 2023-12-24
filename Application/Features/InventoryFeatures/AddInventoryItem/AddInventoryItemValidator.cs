using FluentValidation;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddInventoryItem
{
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
}
