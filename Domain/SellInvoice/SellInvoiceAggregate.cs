using Domain.Core;
using Domain.SellInvoice.Entities;
using Domain.SellInvoice.ValueObjects;
using OctaApi.Domain;
namespace Domain.SellInvoice;

public class SellInvoiceAggregate : AggregateRoot
{
    public bool IsClosed
    {
        get
        {
            var dtNow = DateTime.UtcNow;
            return CreateDate.Value.Year == dtNow.Year &&
                CreateDate.Value.Month == dtNow.Month &&
                CreateDate.Value.Day != dtNow.Day;
        }
    }
    public SellInvoiceSellDate CreateDate { get; set; }
    public SellInvoiceCode Code { get; set; }
    public List<SellInvoiceInventoryItem> InventoryItems { get; set; } = new();
    public List<SellInvoiceService> Services { get; set; } = new();
    public Guid Customer { get; set; }
    public Guid Vehicle { get; set; }
    public bool UseBuyPrice { get; set; }
    public Price Discount { get; set; }
    public SellInvoicecDescription Description { get; set; }
    public List<SellInvoicePayment> Payments { get; set; }
}
