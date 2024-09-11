using MassTransit;

namespace Shared.Events.Payment
{
    public record PaymentStartMessage(string CardNumber, string CardNameSurname, decimal TotalPrice)
        : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}