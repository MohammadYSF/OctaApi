using Application.ReadModels;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;

namespace OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesResponse(List<SellInvoiceRM> Data);
}