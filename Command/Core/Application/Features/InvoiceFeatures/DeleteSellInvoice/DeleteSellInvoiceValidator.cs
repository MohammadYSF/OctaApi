using Command.Core.Application.Features.InvoiceFeatures.DeleteSellInvoice;
using FluentValidation;

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
