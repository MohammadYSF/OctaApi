using MediatR;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed record GetSellInvoicecServicesRequest(Guid InvoiceId) : IRequest<GetSellInvoiceServicesResponse>;
