using OctaShared.Events;

namespace Command.Core.Domain.Core;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
    public IReadOnlyList<DomainEvent> GetDomainEvents()
    {
        return _domainEvents;
    }
    //public virtual IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected virtual void AddDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    public virtual void ClearEvents()
    {
        _domainEvents.Clear();
    }
}

