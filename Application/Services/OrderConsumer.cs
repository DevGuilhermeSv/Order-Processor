using Contracts;
using Domain.Entities;
using Infrastructure;
using MassTransit;
using Product = Domain.Entities.Product;

namespace Application.Services;

public class OrderConsumer : IConsumer<OrderMessage>
{
    private readonly IOrderRepository _repository;

    public OrderConsumer(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<OrderMessage> context)
    {
        var msg = context.Message;

        var pedido = new Order
        {
            Id = msg.Id,
            CreationDate = msg.CreationDate,
            Products = msg.Products.Select( p => new Product()
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Name = p.Name,
                Value = p.Value
            }).ToList()
        };

        await _repository.Create(pedido);
        Console.WriteLine($"Pedido {pedido.Id} consumido da fila.");
    }
}