using MassTransit;

namespace Shared.Events.Stock
{
    public record StockReservedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}