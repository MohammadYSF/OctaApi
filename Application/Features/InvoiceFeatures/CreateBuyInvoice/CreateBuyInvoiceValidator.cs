﻿using FluentValidation;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public class CreateBuyInvoiceValidator : AbstractValidator<CreateBuyInvoiceRequest>
    {
        public CreateBuyInvoiceValidator()
        {
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
