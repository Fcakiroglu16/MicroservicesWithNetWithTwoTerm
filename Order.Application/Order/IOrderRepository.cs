namespace Order.Application.Order
{
    public interface IOrderRepository
    {
        int CreateOrder(Repository.Order order);

        ValueTask<Repository.Order?> GetByIdAsync(int id);
    }
}