using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class ConfigureRepositories
{
    public static void Repositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
    }
}