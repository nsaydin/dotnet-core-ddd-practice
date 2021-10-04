namespace Core.Event
{
    public class SimpleEventContext<T> : IEventContext<T>
        where T : IEvent
    {
        public T Message { get; }
   
        public SimpleEventContext(T message)
        {
            Message = message;
        }
    }
}