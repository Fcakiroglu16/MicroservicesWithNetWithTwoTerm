using MediatR;

namespace Order.Application.Products.Create
{
    public record CreateProductCommand(string Name, int Quantity, decimal Price, int CategoryId) : IRequest<string>;
}