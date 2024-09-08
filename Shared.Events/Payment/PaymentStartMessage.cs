using MassTransit;

namespace Shared.Events.Payment
{
    internal class PaymentStartMessage(Guid correlationId, string CardNumber, string CardNameSurname)
        : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = correlationId;
    }
}