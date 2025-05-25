using Application.Models;

namespace Application.Interfaces;

public interface IOrderService
{
    public Task<OrderResult> FindOrderById(Guid id);
    
    public PagedResult<OrderResult> FindOrders(FilterOrderRequest filterOrderRequest);
}