using MassTransit;
using Order.Domain.Events;
using Order.Domain.Read;

namespace Order.Application.Consumers
{
    internal class ProductCreatedEventConsumer(ISyncWriteRepository syncWriteRepository)
        : IConsumer<ProductCreatedEvent>
    {
        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var productCreatedEvent = context.Message;
            ;

            syncWriteRepository.Create(new ProductWithCategory()
            {
                Id = productCreatedEvent.Id, Name = productCreatedEvent.Name, Quantity = productCreatedEvent.Quantity,
                Price = productCreatedEvent.Price, CategoryName = productCreatedEvent.CategoryName
            });

            return Task.CompletedTask;
        }
    }
}