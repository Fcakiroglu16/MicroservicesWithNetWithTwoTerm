namespace Shared.Events.Order
{
    public record OrderCreatedEvent(
        string OrderCode,
        string BuyerId,
        string CardNumber,
        string CardNameSurname,
        Dictionary<int, int> OrderItems);
}