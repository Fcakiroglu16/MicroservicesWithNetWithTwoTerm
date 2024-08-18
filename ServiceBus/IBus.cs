using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ServiceBus
{
    public interface IBus
    {
        Task Send<T>(T message, string exchangeName) where T : class;

        IModel GetChannel();
    }
}