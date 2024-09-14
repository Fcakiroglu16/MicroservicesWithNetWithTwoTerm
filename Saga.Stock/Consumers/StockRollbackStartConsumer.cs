using MassTransit;
using Shared.Events.Stock;

namespace Saga.Stock.Consumers
{
    public class StockRollbackStartConsumer : IConsumer<StockRollbackStart>
    {
        public Task Consume(ConsumeContext<StockRollbackStart> context)
        {
            //  1. adım : order microservice'sinden ordercode gönder, orderitems bilgilerini al
            //  2. adım : stock veritabanını güncelle

            Console.WriteLine($"stock rollback işlemi gerçekleştir => ordercode={context.Message.OrderCode}");

            return Task.CompletedTask;
        }
    }
}