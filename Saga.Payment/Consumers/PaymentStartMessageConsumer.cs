using MassTransit;
using Shared.Events.Payment;

namespace Saga.Payment.Consumers
{
    public class PaymentStartMessageConsumer(IPublishEndpoint publishEndpoint) : IConsumer<PaymentStartMessage>
    {
        public async Task Consume(ConsumeContext<PaymentStartMessage> context)
        {
            var balance = 3000m;

            if (balance > context.Message.TotalPrice)
            {
                Console.WriteLine($"Ödeme alındı :{context.Message.CorrelationId}");
                await context.Publish(new PaymentCompletedEvent() { CorrelationId = context.Message.CorrelationId });
            }
            else
            {
                await context.Publish<PaymentFailedEvent>(new("Balance is not enough")
                    { CorrelationId = context.Message.CorrelationId });
            }
        }
    }
}