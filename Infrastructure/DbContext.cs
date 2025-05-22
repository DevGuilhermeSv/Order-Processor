using MongoDB.Driver;

namespace Infrastructure;

public class OrderContext<T>
{
    private readonly IMongoCollection<T> _collection;

    public OrderContext(IMongoDatabase database)
    {
        _collection = database.GetCollection<T>(typeof(T).Name);

    }
    public IMongoCollection<T> Collection => _collection;
}