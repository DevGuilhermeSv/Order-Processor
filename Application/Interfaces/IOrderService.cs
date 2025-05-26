using Application.Models;

namespace Application.Interfaces;

public interface IOrderService
{
    public Task<OrderResult> FindOrderById(Guid id);
    
    public Task<PagedResult<OrderResult>> FindOrders(FilterOrderRequest filterOrderRequest);
}