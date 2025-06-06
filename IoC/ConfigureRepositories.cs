using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class ConfigureRepositories
{
    public static void Repositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
        serviceCollection.AddSingleton<OrderContext<Order>>();
    }
}