using MediatR;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed record GetSellInvoicesRequest():IRequest<GetSellInvoicesResponse>;
 
}
