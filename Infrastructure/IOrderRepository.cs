using Domain.Entities;

namespace Infrastructure;

public interface IOrderRepository
{
    Task<Order> GetById(Guid id);
    Task<List<Order>> GetAll();
    Task Create(Order order);
}