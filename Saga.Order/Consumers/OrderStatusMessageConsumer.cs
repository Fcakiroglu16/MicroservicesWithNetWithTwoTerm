using MassTransit;
using Shared.Events.Order;

namespace Saga.Order.Consumers
{
    public class OrderStatusMessageConsumer : IConsumer<OrderStatusMessage>
    {
        public Task Consume(ConsumeContext<OrderStatusMessage> context)
        {
            Console.WriteLine(
                $"{context.Message.OrderCode} - {context.Message.Status} -{context.Message.FailMessage} ");

            return Task.CompletedTask;
            ;
        }
    }
}