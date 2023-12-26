using MediatR;

namespace Command.Core.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice
{
    public sealed record CreateMiscellaneousSellInvoiceRequest():IRequest<CreateMiscellaneousSellInvoiceResponse>;    
}
