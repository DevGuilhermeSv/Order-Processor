using Contracts;

namespace Domain.Entities;

public class Order
{
    private readonly List<Product> _products = new();
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    private  OrderStatus _status;

    public required string Status
    {
        get => _status.ToString();
        set => _status = (OrderStatus)Enum.Parse(typeof(OrderStatus), value);
    }

    public List<Product> Products
    {
        get => _products;
        init => _products = value;
    }

    public decimal ToTalValue => Products.Sum(p => p.Value);
}