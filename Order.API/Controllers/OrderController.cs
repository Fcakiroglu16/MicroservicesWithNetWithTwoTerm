using Microsoft.AspNetCore.Mvc;
using Order.Application.Order;
using Order.Application.Order.CreateOrderUseCase;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var response = await orderService.CreateOrder(request);
            return Ok(response);
        }
    }
}