using MediatR;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoicesByCustomerId;

public record GetSellInvoicesByCustomerIdRequest : IRequest<GetSellInvoicesByCustomerIdResponse>
{
    public string CustomerCode { get; set; } = string.Empty;
}
