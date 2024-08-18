using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await orderService.Create();
            return Ok();
        }
    }
}