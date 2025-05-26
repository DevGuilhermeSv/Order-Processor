using Bogus;
using Contracts;
using MassTransit;

namespace ProducerOrder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger,IConfiguration config, IBus bus)
    {
        _logger = logger;
        _config = config;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int total = int.Parse(_config["Carga:TotalPedidos"]!);
        int delay = int.Parse(_config["Carga:IntervaloMs"]!);

        var fakerProduct = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Value, f => decimal.Parse(f.Commerce.Price(10, 100)));
        var fakerOrder = new Faker<OrderMessage>()
            .RuleFor(o => o.Products, () => fakerProduct.Generate(new Random().Next(1, 5)))
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor( o => o.Id, f => Guid.NewGuid() );

        Console.WriteLine($"🚀 Enviando {total} pedidos...");
        for (int i = 0; i < total && !stoppingToken.IsCancellationRequested; i++)
        {
            var order = fakerOrder.Generate();

            await _bus.Publish<OrderMessage>(order, stoppingToken);
            Console.WriteLine($"✅ Pedido {order.Id} enviado");

            if (delay > 0)
                await Task.Delay(delay, stoppingToken);
        }

        Console.WriteLine("🏁 Teste de carga finalizado.");
    }
}