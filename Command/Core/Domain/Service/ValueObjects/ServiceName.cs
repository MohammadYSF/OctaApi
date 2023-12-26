using Command.Core.Domain.Core;

namespace Command.Core.Domain.Service.ValueObjects;
public sealed class ServiceName : ValueObject<ServiceName>
{
    public ServiceName(string value)
    {
        Value = value;
    }
    public string Value { get; set; }
    protected override bool EqualsCore(ServiceName other)
    {
        return other.Value == Value;
    }

    protected override int GetHashCodeCore()
    {
        throw new NotImplementedException();
    }
}
