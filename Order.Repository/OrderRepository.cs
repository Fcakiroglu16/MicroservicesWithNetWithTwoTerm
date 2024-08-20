using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Order;

namespace Order.Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public int CreateOrder(Order order)
        {
            context.Orders.Add(order);

            context.SaveChanges();

            return order.Id;
        }
    }
}