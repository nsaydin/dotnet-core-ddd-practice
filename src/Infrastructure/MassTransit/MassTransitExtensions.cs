using System;
using Core.Event;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MassTransit
{
    public static class MassTransitExtensions
    {
        public static void RegisterEventHandler<TEvent, TEventHandler>(this IReceiveEndpointConfigurator configurator,
            IServiceProvider serviceProvider)
            where TEvent : class, IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            configurator.Handler<TEvent>(async context =>
            {
                using var serviceScope = serviceProvider.CreateScope();
                try
                {
                    var eventContext = new SimpleEventContext<TEvent>(context.Message);
                    await serviceScope.ServiceProvider.GetRequiredService<TEventHandler>().Handle(eventContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}