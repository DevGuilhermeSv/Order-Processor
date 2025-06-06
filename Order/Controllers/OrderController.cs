using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderResult), 200)]

    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.FindOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OrderResult>), 200)]
    public async Task<IActionResult> GetAllOrders([FromQuery] FilterOrderRequest filterOrderRequest)
    {
        var orders = await _orderService.FindOrders(filterOrderRequest);
        return Ok(orders);
    }
}