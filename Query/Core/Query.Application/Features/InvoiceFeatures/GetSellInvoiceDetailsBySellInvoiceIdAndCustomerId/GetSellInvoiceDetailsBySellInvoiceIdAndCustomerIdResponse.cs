using OctaShared.ReadModels;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceDetailsBySellInvoiceIdAndCustomerId;
public record GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse
{
    public SellInvoiceRM? SellInvoiceRM { get; set; }
    public List<SellInvoiceServiceRM> sellInvoiceServiceRMs { get; set; } = new();
    public List<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMs { get; set; } = new();
}
