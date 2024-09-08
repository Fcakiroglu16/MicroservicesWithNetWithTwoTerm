using MassTransit;

namespace Shared.Events.Stock
{
    public record StockReservedEvent(Guid CorrelationId) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = CorrelationId;
    }
}