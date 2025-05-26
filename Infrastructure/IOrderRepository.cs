using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure;

public interface IOrderRepository
{
    Task<Order> GetById(Guid id);
    IFindFluent<Order,Order> GetAll(FilterDefinition<Order> findOptions);
    Task Create(Order order);
    
}