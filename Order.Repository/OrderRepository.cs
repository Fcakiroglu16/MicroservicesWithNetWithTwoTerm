using Order.Application.Order;
using Order.Repository;

namespace Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public int CreateOrder(Order.Repository.Order order)
        {
            context.Orders.Add(order);

            context.SaveChanges();

            return order.Id;
        }
    }
}