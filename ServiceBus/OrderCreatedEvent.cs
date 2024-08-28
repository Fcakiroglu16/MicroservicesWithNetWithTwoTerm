namespace ServiceBus;

public record OrderCreatedEvent(int OrderId, Dictionary<int, int> StockInfo);