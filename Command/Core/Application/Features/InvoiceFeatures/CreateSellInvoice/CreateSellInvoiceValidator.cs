using FluentValidation;
using OctaShared.DTOs.Request;

namespace Command.Core.Application.Features.InvoiceFeatures.CreateInvoice;
public class CreateSellInvoiceValidator : AbstractValidator<CreateSellInvoiceRequest>
{
    public CreateSellInvoiceValidator()
    {
        RuleFor(a => a.VehicleId)
            .NotNull()
            .NotEmpty();
        RuleFor(a => a.CustomerId)
    .NotNull()
    .NotEmpty();
    }
}
