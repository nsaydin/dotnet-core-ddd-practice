using System;
using Core.Event;

namespace Core.Domain.Order
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; set; }

        public OrderCreated()
        {
        }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}