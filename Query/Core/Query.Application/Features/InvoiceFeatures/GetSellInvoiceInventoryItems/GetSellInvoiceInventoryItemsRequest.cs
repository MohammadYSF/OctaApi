using MediatR;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItemsRequest(Guid InvoiceId) :IRequest<GetSellInvoiceInventoryItemsResponse>;   
}
