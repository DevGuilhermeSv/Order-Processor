using System.Linq.Expressions;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infrastructure;

namespace Application.Services;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResult> FindOrderById(Guid id)
    {
        var order = await _orderRepository.GetById(id);

        if (order == null)
        {
            return null;
        }

        // Converte o pedido para um OrderResult
        var orderResult = new OrderResult
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            Products = order.Products,
            ToTalValue = order.ToTalValue
        };

        return orderResult;
    }

    public PagedResult<OrderResult> FindOrders(FilterOrderRequest filterOrderRequest)
    {
        //TODO 
        throw new NotImplementedException();
    }
}