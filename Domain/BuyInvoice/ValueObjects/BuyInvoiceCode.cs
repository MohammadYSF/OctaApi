using OctaApi.Domain.Common;
namespace Domain.BuyInvoice.ValueObjects;
public sealed class BuyInvoiceCode : ValueObject<BuyInvoiceCode>
{
    public BuyInvoiceCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(BuyInvoiceCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}