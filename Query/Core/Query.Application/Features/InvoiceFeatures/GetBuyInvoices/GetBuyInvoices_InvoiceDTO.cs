namespace Query.Application.Features.InvoiceFeatures.GetBuyInvoices;

public sealed record GetBuyInvoices_InvoiceDTO(int RowNumber, Guid InvoiceId, string InvoiceCode, DateTime InvoiceDate, string InvoiceDateString, string SellerName, float InvoiceTotalPrice, float InvoicePaidAmount);
