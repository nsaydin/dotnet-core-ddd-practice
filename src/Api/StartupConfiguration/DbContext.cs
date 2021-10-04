using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfiguration
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<AppDbContext>((_, optionsBuilder) =>
            {
                optionsBuilder.UseInMemoryDatabase("Fake");
            });
        }
    }
}