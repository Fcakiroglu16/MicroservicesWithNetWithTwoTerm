using MassTransit;

namespace Shared.Events.Stock
{
    public record StockReserveStartMessage(Dictionary<int, int> StockItems) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}