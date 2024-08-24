using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Order.Commands.Create;
using Order.Application.Order.Queries;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMediatRController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateCommand orderCreateCommand)
        {
            var response = await mediator.Send(orderCreateCommand);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(response);
        }

        // GetOrderPagedList/10.
        [HttpGet("GetOrderPagedList{pageNumber:int}/{PageSize:int}")]
        public async Task<IActionResult> GetOrderPagedList(int pageNumber, int PageSize)
        {
            return Ok();
        }
    }
}