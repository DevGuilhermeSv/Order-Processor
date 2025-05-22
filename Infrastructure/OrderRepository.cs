using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure;

public class OrderRepository: IOrderRepository
{
    private readonly OrderContext<Order> _context;

    public OrderRepository(OrderContext<Order> context)
    {
        _context = context;
    }

    public Task<Order> GetById(Guid id)
    {
        return _context.Collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Order>> GetAll()
    {
        return  _context.Collection.Find(p => true).ToListAsync();
    }

    public  Task Create(Order order)
    {
        return _context.Collection.InsertOneAsync(order);
    }
}