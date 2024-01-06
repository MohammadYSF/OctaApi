using Query.Application.ReadModels;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed record GetSellInvoiceServicesResponse(List<SellInvoiceServiceRM> Data);
