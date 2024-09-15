namespace Shared.Bus
{
    public record ProductAddedEvent(int Id, string Name, decimal Price, string TraceId);
}