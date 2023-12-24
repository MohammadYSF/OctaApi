using FluentValidation;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
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
}
