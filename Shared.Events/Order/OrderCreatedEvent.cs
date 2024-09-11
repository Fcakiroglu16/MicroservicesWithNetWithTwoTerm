namespace Shared.Events.Order
{
    public record OrderCreatedEvent(
        string OrderCode,
        string BuyerId,
        string CardNumber,
        string CardNameSurname,
        decimal TotalPrice,
        Dictionary<int, int> OrderItems);
}