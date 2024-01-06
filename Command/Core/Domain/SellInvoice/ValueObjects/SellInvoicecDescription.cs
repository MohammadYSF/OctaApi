using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.ValueObjects;
public sealed class SellInvoicecDescription : ValueObject<SellInvoicecDescription>
{
    public SellInvoicecDescription(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(SellInvoicecDescription other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
