using System.Collections.Generic;
using Core.Event;

namespace Core.Domain.Entity
{
    public interface IAggregateRoot : IEvent
    {
        IEnumerable<IEvent> DomainEvents { get; }
        void ClearEvents();
    }
}