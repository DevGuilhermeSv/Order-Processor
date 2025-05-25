using Application.Models;

namespace Application.Interfaces;

public interface IOrderService
{
    public OrderResult FindOrderById(Guid id);
    
    public PagedResult<OrderResult> FindOrders(FilterOrderRequest filterOrderRequest);
}