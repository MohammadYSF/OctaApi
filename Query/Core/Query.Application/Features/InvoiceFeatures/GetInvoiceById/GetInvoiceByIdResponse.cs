﻿using Application.ReadModels;
namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById;
public sealed record GetInvoiceByIdResponse(SellInvoiceRM SellInvoiceRM, SellInvoiceDescriptionRM SellInvoiceDescriptionRM);
