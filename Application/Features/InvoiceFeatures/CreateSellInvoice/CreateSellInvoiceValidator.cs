using FluentValidation;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;

namespace OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
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
