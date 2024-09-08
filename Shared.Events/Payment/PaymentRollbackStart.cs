namespace Shared.Events.Payment
{
    public record PaymentRollbackStart(string CardNumber, decimal Price);
}