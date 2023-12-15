using OctaApi.Domain.Common;

namespace Domain.Service.ValueObjects;
public sealed class ServiceCode : ValueObject<ServiceCode>
{
    public ServiceCode(int value)
    {
        Value = value;
    }
    public int Value { get; set; }
    protected override bool EqualsCore(ServiceCode other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
