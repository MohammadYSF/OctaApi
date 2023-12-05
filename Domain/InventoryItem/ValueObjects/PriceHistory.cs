using OctaApi.Domain.Common;

namespace OctaApi.Domain.InventoryItem.ValueObjects;
public sealed class PriceHistory : ValueObject<PriceHistory>
{
    public PriceHistory(Price price , DateTime dateTime)
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
}