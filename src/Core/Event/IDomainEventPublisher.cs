using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Event
{
    public interface IDomainEventPublisher
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        Task PublishEvents<TEvent>(IEnumerable<TEvent> events) where TEvent : IEvent;
    }
 }