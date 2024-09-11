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
                Console.WriteLine($"stock'tan düştü :{context.Message.CorrelationId}");
                await context.Publish<StockReservedEvent>(new
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }
            else
            {
                await context.Publish<StockNotReservedEvent>(new
                {
                    CorrelationId = context.Message.CorrelationId,
                    Reason = "Stock is not enough"
                });
            }
        }
    }
}