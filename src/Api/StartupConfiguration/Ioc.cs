using Application.Mapping;
using Core;
using Core.Domain.Product;
using Core.Event;
using Core.Extensions;
using Core.Listeners;
using Core.WorkTime;
using Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfiguration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIoc(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Application.Mapping.MapsterMapper>();
            services.AddSingleton<IWorkTime, WorkTime>();

            // Data Access
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(AppDbContext))
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Domain Services
            services.Scan(s => s
                .FromAssembliesOf(typeof(IProductDomainService))
                .AddClasses(c => c.Where(t => t.Name.EndsWith("DomainService")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Event Listeners
            services.Scan(s => s
                .FromAssembliesOf(typeof(ChangeProductStockEventHandler))
                .AddClasses(classes => classes.Where(x => x.IsAssignableToGenericType(typeof(IEventHandler<>))))
                .AsSelf()
                .WithTransientLifetime());

            return services;
        }
    }
}