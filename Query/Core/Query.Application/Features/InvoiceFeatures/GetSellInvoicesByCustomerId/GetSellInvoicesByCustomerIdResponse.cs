using OctaShared.ReadModels;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoicesByCustomerId;

public record GetSellInvoicesByCustomerIdResponse
{
    public int TotalCount { get; set; }
    public List<SellInvoiceRM> SellInvoiceRMs { get; set; }
}
