
namespace Command.Core.Domain.Core;
public sealed class PriceHistory : ValueObject<PriceHistory>
{
    public PriceHistory(Price price, DateTime dateTime)
    {
        Price = price;
        DateTime = dateTime;
    }
    public DateTime DateTime { get; set; }
    public Price Price { get; set; }
    protected override bool EqualsCore(PriceHistory other)
    {
        return other.Price == Price && other.DateTime == DateTime;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
    public override string ToString()
    {
        return Price.ToString() + "|" + DateTime.ToString();
    }

    public static explicit operator PriceHistory(string v)
    {
        var x = v.Split('|');
        long val = long.Parse(x[0]);
        DateTime dt = DateTime.Parse(x[1]);
        return new PriceHistory(new Price(val), dt);
    }
}