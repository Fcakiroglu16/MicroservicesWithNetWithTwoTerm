using MassTransit;

namespace Shared.Events.Stock
{
    public record StockNotReservedEvent(string Reason) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}