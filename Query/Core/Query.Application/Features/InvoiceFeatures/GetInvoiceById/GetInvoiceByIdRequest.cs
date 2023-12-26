using MediatR;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById
{
    public sealed record GetInvoiceByIdRequest(Guid InvoiceId):IRequest<GetInvoiceByIdResponse>;
}
