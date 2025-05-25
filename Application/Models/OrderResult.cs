using Domain.Entities;

namespace Application.Models;

public class OrderResult
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Product> Products { get; set; }

    public decimal ToTalValue { get; set; }
}