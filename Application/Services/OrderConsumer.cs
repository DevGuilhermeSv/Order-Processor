using Application.Models;
using Domain.Entities;
using Infrastructure;
using MassTransit;

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
            Products = msg.Products
        };

        await _repository.Create(pedido);
        Console.WriteLine($"Pedido {pedido.Id} consumido da fila.");
    }
}