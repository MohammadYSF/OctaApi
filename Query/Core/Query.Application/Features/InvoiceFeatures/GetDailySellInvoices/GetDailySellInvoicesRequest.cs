using MediatR;
namespace Query.Application.Features.InvoiceFeatures.GetDailySellInvoices;

public sealed record GetDailySellInvoicesRequest() : IRequest<GetDailySellInvoicesResponse>;
