using System.Reflection;
using Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfiguration
{
    public static class MediatRService
    {
        public static IServiceCollection AddMyMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateProductCommand).Assembly);

            return services;
        }
    }
}