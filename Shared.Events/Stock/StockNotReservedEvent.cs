using MassTransit;

namespace Shared.Events.Stock
{
    public record StockNotReservedEvent(Guid CorrelationId, string Reason) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = CorrelationId;
    }
}