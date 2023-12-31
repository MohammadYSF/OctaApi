﻿namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed record GetSellInvoices_InvoiceDTO(int RowNumber, Guid InvoiceId, string InvoiceCode, DateTime InvoiceDate, string InvoiceDateString, string VehicleName,
        string CustomerName, float InvoiceTotalPrice, float InvoicePaidAmount);
}
