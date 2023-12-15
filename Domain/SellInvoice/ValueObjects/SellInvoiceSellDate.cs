using OctaApi.Domain.Common;
namespace Domain.SellInvoice.ValueObjects;
public sealed class SellInvoiceSellDate : ValueObject<SellInvoiceSellDate>
{
    public SellInvoiceSellDate(DateTime value)
    {
        Value = value;
    }
    public DateTime Value { get; set; }
    protected override bool EqualsCore(SellInvoiceSellDate other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}