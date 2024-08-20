using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.CreateOrderUseCase
{
    public record CreateOrderRequest(string Name, int Quantity, decimal Price);
}