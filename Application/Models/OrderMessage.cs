using Domain.Entities;

namespace Application.Models;

public class OrderMessage
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Product> Products { get; set; }
}