using FluentValidation;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.DeleteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
    public class DeleteServiceValidator : AbstractValidator<DeleteServiceRequest>
    {
        public DeleteServiceValidator()
        {
            RuleFor(x => x.Code)
          .NotNull()
          .GreaterThan(0);
        }
    }
}
