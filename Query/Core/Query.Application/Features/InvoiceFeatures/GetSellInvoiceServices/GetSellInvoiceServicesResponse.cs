using OctaShared.ReadModels;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed record GetSellInvoiceServicesResponse(List<SellInvoiceServiceRM> Data);
