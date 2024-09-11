using MassTransit;

namespace Shared.Events.Payment
{
    public record PaymentFailedEvent(string Reason) : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}