using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.CreateOrderUseCase
{
    public record OrderCreatedEvent(int OrderId, int Quantity);
}