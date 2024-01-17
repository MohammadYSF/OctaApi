using OctaShared.ReadModels;
namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
public sealed record GetSellInvoicesResponse(List<SellInvoiceRM> Data);

