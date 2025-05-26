using System.Linq.Expressions;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using MongoDB.Driver;

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

    public async Task<PagedResult<OrderResult>> FindOrders(FilterOrderRequest filterOrderRequest)
    {
        var builder = Builders<Order>.Filter;
        var filters = new List<FilterDefinition<Order>>();

        if (!string.IsNullOrEmpty(filterOrderRequest.ProductName))
        {
            filters.Add(builder.Where(p =>
                p.Products.Any(p => p.Name.ToLower() == filterOrderRequest.ProductName.ToLower())));
        }

        if (filterOrderRequest.Status is not null)
        {
            filters.Add(builder.Where(p => p.Status == filterOrderRequest.Status.ToString()));
        }

        var orders =  _orderRepository.GetAll(builder.And(filters));
        var skip = (filterOrderRequest.Page - 1) * filterOrderRequest.PageSize;
        var count = await orders.CountDocumentsAsync();

        orders
            .Sort(DinamicOrdanition(filterOrderRequest.OrderBy, true))
            .Skip(skip)
            .Limit(filterOrderRequest.PageSize);
        var orderResult = orders.ToEnumerable().Select(order => new OrderResult()
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            Products = order.Products,
            ToTalValue = order.ToTalValue
        });

        return new PagedResult<OrderResult>(orderResult, (int)count, filterOrderRequest.Page,
            filterOrderRequest.PageSize);
    }

    public static SortDefinition<Order> DinamicOrdanition(string? campo, bool descendente)
    {
        var builder = Builders<Order>.Sort;

        if (string.IsNullOrWhiteSpace(campo))
            return builder.Ascending("_id");

        // Cria ordenação com base no nome da propriedade
        return descendente
            ? builder.Descending(new StringFieldDefinition<Order>(campo))
            : builder.Ascending(new StringFieldDefinition<Order>(campo));
    }
}