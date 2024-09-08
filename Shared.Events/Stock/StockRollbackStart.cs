using MassTransit;

namespace Shared.Events.Stock
{
    public record StockRollbackStart(Dictionary<int, int> StockInfo);
}