using MassTransit;

namespace Shared.Events.Payment
{
    public class PaymentFailedEvent(Guid correlationId, string Reason) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = correlationId;
    }
}