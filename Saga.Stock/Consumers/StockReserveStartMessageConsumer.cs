using MassTransit;
using Shared.Events.Stock;

namespace Saga.Stock.Consumers
{
    public class StockReserveStartMessageConsumer : IConsumer<StockReserveStartMessage>
    {
        public async Task Consume(ConsumeContext<StockReserveStartMessage> context)
        {
            var hasStock = true;

            if (hasStock)
            {
                Console.WriteLine($"Stock is  enough :{context.Message.CorrelationId}");
                await context.Publish<StockReservedEvent>(new
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }
            else
            {
                Console.WriteLine($"Stock is not enough :{context.Message.CorrelationId}");
                await context.Publish(new StockNotReservedEvent("Stock is not enough")
                    { CorrelationId = context.Message.CorrelationId });
            }
        }
    }
}