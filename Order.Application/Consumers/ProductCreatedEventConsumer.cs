using MassTransit;
using Order.Domain.Events;
using Order.Domain.Read;

namespace Order.Application.Consumers;

public class ProductCreatedEventConsumer(ISyncWriteRepository syncWriteRepository)
    : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var productCreatedEvent = context.Message;


        await syncWriteRepository.Create(new ProductWithCategory
        {
            Id = productCreatedEvent.Id, Name = productCreatedEvent.Name, Quantity = productCreatedEvent.Quantity,
            Price = productCreatedEvent.Price, CategoryName = productCreatedEvent.CategoryName
        });
    }
}