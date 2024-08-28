using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Products.Create
{
    public record CreateProductCommand(string Name, int Quantity, decimal Price, int CategoryId) : IRequest<string>;
}