using MassTransit;

namespace Shared.Events.Payment
{
    public record PaymentCompletedEvent(Guid CorrelationId) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; } = CorrelationId;
    }
}