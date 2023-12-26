using MediatR;

namespace Command.Core.Application.Features.InvoiceFeatures.DeleteSellInvoice;

public sealed record DeleteSellInvoiceRequest(Guid Id):IRequest<DeleteSellInvoiceResponse>;

