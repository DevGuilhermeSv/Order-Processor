using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contracts;

public class OrderMessage
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } 
    public DateTime CreationDate { get; set; }
    public required List<Product> Products { get; set; }
}

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}