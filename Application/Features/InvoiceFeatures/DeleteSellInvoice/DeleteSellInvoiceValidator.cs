using FluentValidation;
using OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoice;

namespace Application.Features.InvoiceFeatures.DeleteSellInvoiuce;
public class DeleteSellInvoiceValidator : AbstractValidator<DeleteSellInvoiceRequest>
{
    public DeleteSellInvoiceValidator()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEmpty();
    }
}
