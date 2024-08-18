using MassTransit;
using ServiceBus;
using System.Data;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine("Consume Methodu Çalıştı");
            //throw new DBConcurrencyException();

            Console.WriteLine($"(Masstransit) Gelen Event:{context.Message.OrderId}");

            return Task.CompletedTask;
        }
    }
}