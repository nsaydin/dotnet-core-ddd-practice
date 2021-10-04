using Core.Domain.Campaign;
using Core.Domain.Order;
using Core.Event;
using Core.Listeners;
using Infrastructure.MassTransit;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfiguration
{
    public static class MassTransitService
    {
        public static IServiceCollection AddInMemoryBus(this IServiceCollection services)
        {
            services.AddMassTransit(appProvider =>
            {
                return Bus.Factory.CreateUsingInMemory(
                    configurator =>
                    {
                        configurator.ReceiveEndpoint("order_events",
                            ep =>
                            {
                                ep.RegisterEventHandler<OrderCreated, ChangeProductStockEventHandler>(appProvider);
                                ep.RegisterEventHandler<CampaignApplied, CampaignAppliedEventHandler>(appProvider);
                            });
                    });
            });

            services.AddScoped<IDomainEventPublisher, MassTransitDomainEventPublisher>();

            return services;
        }
    }
}