using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Event;
using MassTransit;

namespace Infrastructure.MassTransit
{
    public class MassTransitDomainEventPublisher : IDomainEventPublisher
    {
        private readonly IBusControl _bus;

        public MassTransitDomainEventPublisher(IBusControl bus)
        {
            _bus = bus;
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return _bus.Publish(@event, @event.GetType());
        }

        public async Task PublishEvents<TEvent>(IEnumerable<TEvent> events) where TEvent : IEvent
        {
            foreach (var @event in events)
            {
                await Publish(@event);
            }
        }
    }
}