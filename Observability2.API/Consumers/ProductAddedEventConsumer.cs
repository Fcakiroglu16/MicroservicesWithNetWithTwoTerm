using System.Collections.Concurrent;
using System.Diagnostics;
using MassTransit;
using Observability.API;
using Shared.Bus;

namespace Observability2.API.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Gelen Mesaj (ProductId={context.Message.Id})");
            //var activityTraceId = ActivityTraceId.CreateFromString(context.Message.TraceId);
            //var spanId = ActivitySpanId.CreateFromString("");
            //var activityContext = new ActivityContext(activityTraceId, spanId, ActivityTraceFlags.Recorded);


            //using (var activity =
            //       ActivitySourceProvider.ActivitySource.StartActivity(ActivityKind.Consumer, activityContext))
            //{
            //}

            return Task.CompletedTask;
        }
    }
}