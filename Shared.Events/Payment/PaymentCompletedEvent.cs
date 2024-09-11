using MassTransit;

namespace Shared.Events.Payment
{
    public record PaymentCompletedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}