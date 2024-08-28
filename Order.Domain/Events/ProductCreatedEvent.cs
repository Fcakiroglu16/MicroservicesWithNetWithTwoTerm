namespace Order.Domain.Events;

public record ProductCreatedEvent(string Id, string Name, int Quantity, decimal Price, string CategoryName);