using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Order.Queries
{
    public record GetOrderByIdQueryResponse(int Id, string Name, int Quantity, decimal Price);
}