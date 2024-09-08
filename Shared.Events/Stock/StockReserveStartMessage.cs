using MassTransit;

namespace Shared.Events.Stock
{
    internal class StockReserveStartMessage(Guid correlationId, Dictionary<int, int> StockItems) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = correlationId;
    }
}