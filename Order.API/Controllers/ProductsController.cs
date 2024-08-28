using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Products;
using Order.Application.Products.Create;
using Order.Domain.Write;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository,
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await productReadRepository.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(string name)
        {
            var x = await productWriteRepository.AddCategory(new Category() { Name = name });

            return Ok(x);
        }
    }
}