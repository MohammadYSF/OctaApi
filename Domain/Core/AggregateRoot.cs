using System.Collections.Generic;
using OctaApi.Domain.Common;

namespace OctaApi.Domain;

    public abstract class AggregateRoot : Entity
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public virtual IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        protected virtual void AddDomainEvent(DomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }

