using MediatR;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceDetailsBySellInvoiceIdAndCustomerId;

public record GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest : IRequest<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse>
{
    public string CustomerCode { get; set; } = string.Empty;
    public Guid SellInvoiceId { get; set; }
}
