using Order.Application.Order.CreateOrderUseCase;

namespace Order.Application
{
    public interface IBusService
    {
        Task PublishAsync(OrderCreatedEvent orderCreatedEvent);
    }
}