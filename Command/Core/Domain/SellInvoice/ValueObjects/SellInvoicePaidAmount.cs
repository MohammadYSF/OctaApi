using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.ValueObjects;

public sealed class SellInvoicePaidAmount : ValueObject<SellInvoicePaidAmount>
{
    public SellInvoicePaidAmount(long value)
    {
        Value = value;
    }
    public long Value { get; set; }
    protected override bool EqualsCore(SellInvoicePaidAmount other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
