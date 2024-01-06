
using Query.Application.ReadModels;
namespace Query.Application.Features.InvoiceFeatures.GetDailySellInvoices;

public sealed record GetDailySellInvoicesResponse(List<SellInvoiceRM> Data);