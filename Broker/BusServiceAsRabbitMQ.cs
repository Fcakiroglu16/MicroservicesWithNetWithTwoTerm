using Order.Application;
using Order.Application.Order.CreateOrderUseCase;

namespace Broker
{
    public class BusServiceAsRabbitMq : IBusService
    {
        public Task PublishAsync(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine("Message gönderildi (RabbitMQ)");

            return Task.CompletedTask;
        }
    }
}