using Command.Core.Domain.Core;

namespace Command.Core.Domain.SellInvoice.ValueObjects;
public class SellInvoicePaymentDate : ValueObject<SellInvoicePaymentDate>
{
    public SellInvoicePaymentDate(DateTime value)
    {
        Value = value;
    }
    public DateTime Value { get; set; }
    protected override bool EqualsCore(SellInvoicePaymentDate other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
