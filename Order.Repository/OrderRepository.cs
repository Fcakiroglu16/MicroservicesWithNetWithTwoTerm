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

        public ValueTask<Order.Repository.Order?> GetByIdAsync(int id)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

            return context.Orders.FindAsync(id, cancellationTokenSource.Token);
        }
    }
}