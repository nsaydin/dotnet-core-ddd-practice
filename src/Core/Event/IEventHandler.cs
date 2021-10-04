using System.Threading.Tasks;

namespace Core.Event
{
    public interface IEventHandler<in TEvent>  where TEvent : class, IEvent
    {
        Task Handle(IEventContext<TEvent> context);
    }
}