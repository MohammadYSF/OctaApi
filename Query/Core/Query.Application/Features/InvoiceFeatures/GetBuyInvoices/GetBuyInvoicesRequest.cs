using MediatR;
namespace Query.Application.Features.InvoiceFeatures.GetBuyInvoices;

public sealed record GetBuyInvoicesRequest() : IRequest<GetBuyInvoicesResponse>;

