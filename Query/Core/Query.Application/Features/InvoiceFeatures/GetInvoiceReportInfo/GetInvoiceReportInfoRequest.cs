using MediatR;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed record GetInvoiceReportInfoRequest(Guid InvoiceId) : IRequest<GetInvoiceReportInfoResponse>;
}
