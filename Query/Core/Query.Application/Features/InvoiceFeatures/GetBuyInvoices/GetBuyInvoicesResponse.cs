﻿using Query.Application.ReadModels;

namespace Query.Application.Features.InvoiceFeatures.GetBuyInvoices;
public sealed record GetBuyInvoicesResponse(List<BuyInvoiceRM> Data);
