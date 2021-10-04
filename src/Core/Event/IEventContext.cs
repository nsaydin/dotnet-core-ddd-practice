namespace Core.Event
{
    public interface IEventContext<out TEvent> where TEvent : IEvent
    {
        TEvent Message { get; }
    }
}