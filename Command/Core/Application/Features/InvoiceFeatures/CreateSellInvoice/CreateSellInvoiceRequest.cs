using MediatR;

namespace Command.Core.Application.Features.InvoiceFeatures.CreateInvoice
{
    public sealed record CreateSellInvoiceRequest(Guid VehicleId, Guid CustomerId) : IRequest<CreateSellInvoiceResponse>;
}
