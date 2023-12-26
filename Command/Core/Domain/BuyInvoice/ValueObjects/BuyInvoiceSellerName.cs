
using Command.Core.Domain.Core;

namespace Command.Core.Domain.BuyInvoice.ValueObjects;
public sealed class BuyInvoiceSellerName : ValueObject<BuyInvoiceSellerName>
{
    public BuyInvoiceSellerName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(BuyInvoiceSellerName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}