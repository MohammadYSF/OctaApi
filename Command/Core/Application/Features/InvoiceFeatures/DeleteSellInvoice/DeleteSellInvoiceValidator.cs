using FluentValidation;
using OctaShared.DTOs.Request;

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
