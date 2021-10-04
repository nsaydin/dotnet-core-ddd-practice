using System;
using System.Collections.Generic;
using Core.Event;

namespace Core.Domain.Entity
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        private readonly List<IEvent> _domainEvents = new List<IEvent>();
        public IEnumerable<IEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}