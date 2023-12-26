using MediatR;

namespace Query.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;

public sealed record GetInvoicePaymentInfoRequest(Guid InvoiceId) : IRequest<GetInvoicePaymentInfoResponse>;    
