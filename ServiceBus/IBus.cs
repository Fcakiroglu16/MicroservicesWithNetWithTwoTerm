using RabbitMQ.Client;

namespace ServiceBus;

public interface IBus
{
    Task Send<T>(T message, string exchangeName) where T : class;

    IModel GetChannel();
}