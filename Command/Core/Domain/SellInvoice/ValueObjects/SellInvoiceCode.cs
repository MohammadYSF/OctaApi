using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.ValueObjects;
public sealed class SellInvoiceCode : ValueObject<SellInvoiceCode>
{
    public SellInvoiceCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(SellInvoiceCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}