using Command.Core.Domain.Core;

namespace Command.Core.Domain.BuyInvoice.ValueObjects;
public sealed class BuyInvoiceBuyDate : ValueObject<BuyInvoiceBuyDate>
{
    public BuyInvoiceBuyDate(DateTime value)
    {
        Value = value;
    }
    public DateTime Value { get; set; }
    protected override bool EqualsCore(BuyInvoiceBuyDate other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}