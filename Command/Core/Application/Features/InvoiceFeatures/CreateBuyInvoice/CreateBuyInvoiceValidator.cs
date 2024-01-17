using FluentValidation;
using OctaShared.DTOs.Request;

namespace Command.Core.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public class CreateBuyInvoiceValidator : AbstractValidator<CreateBuyInvoiceRequest>
    {
        public CreateBuyInvoiceValidator()
        {
            RuleFor(a => a.Dtos)
                .NotNull()
                .NotEmpty();
            RuleForEach(a => a.Dtos);
        }
    }
    public class CreateBuyInvoice_InventoryItemDTO_Validator : AbstractValidator<CreateBuyInvoice_InventoryItemDTO>
    {
        public CreateBuyInvoice_InventoryItemDTO_Validator()
        {
            RuleFor(a => a.SellPrice)
                .NotNull()
                .GreaterThan(0);
            RuleFor(a => a.BuyPrice);
        }
    }
}
