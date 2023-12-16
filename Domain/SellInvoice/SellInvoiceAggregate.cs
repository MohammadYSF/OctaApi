using Domain.BuyInvoice.ValueObjects;
using Domain.Core;
using Domain.SellInvoice.Entities;
using Domain.SellInvoice.ValueObjects;
using OctaApi.Domain;

namespace Domain.SellInvoice;

public class SellInvoiceAggregate : AggregateRoot
{
    public SellInvoiceSellDate BuyDate { get; set; }
    public SellInvoiceCode Code { get; set; }
    List<Guid> InventoryItems { get; set; } = new();
    List<Guid> Services { get; set; } = new();
    public Guid Customer{ get; set; }
    public Guid Vehicle{ get; set; }
    public bool UseBuyPrice { get; set; }
    public Price Discount{ get; set; }
    public SellInvoicecDescription Description{ get; set; }
    public List<SellInvoicePayment> Payments{ get; set; }
}
