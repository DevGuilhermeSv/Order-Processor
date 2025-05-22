namespace Domain.Entities;

public class Order
{
    private readonly List<Product> _products = new();
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public List<Product> Products
    {
        get => _products;
        init => _products = value;
    }

    public decimal ToTalValue => Products.Sum(p => p.Value);
}