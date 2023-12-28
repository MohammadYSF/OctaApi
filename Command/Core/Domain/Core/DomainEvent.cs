namespace Command.Core.Domain.Core;
[Serializable]
public class DomainEvent
{
    public string Type { get; set; }
    public DomainEvent(string type)
    {
        this.Type = type;
    }
    public Guid EventId { get; set; }
}

