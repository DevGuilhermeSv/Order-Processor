using Contracts;

namespace Application.Models;

public class FilterOrderRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? ProductName { get; set; }
    public OrderStatus? Status { get; set; }
    public string? OrderBy { get; set; }
}