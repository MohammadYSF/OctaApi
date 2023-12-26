using Application.ReadModels;
namespace OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices;
public sealed record GetBuyInvoicesResponse(List<BuyInvoiceRM> Data);    
