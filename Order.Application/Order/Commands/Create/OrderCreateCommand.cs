using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Order.Commands.Create
{
    public record OrderCreateCommand(string Name, int Quantity, decimal Price) : IRequest<OrderCreateCommandResponse>;


    public record OrderCreateCommandResponse(int Id);
}