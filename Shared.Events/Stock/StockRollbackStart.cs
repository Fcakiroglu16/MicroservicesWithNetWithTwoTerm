using MassTransit;

namespace Shared.Events.Stock
{
    public record StockRollbackStart(string OrderCode);
}