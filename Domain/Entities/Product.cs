namespace Domain.Entities;

public class Product
{
    public Product()
    {
    }
    public Product(Guid orderId, decimal value)
    {
        OrderId = orderId;
        Value = value;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}