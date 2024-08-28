namespace ServiceBus;

public class BusConst
{
    public const string OrderCreatedEventExchange = "order.api.order.created.event.exchange";
    public const string StockOrderCreatedEventQueue = "stock.api.order.created.event.queue";

    public const string StockOrderCreatedEventQueueWithMassTransit =
        "stock.api.order.created.event.masstransit.queue";
}