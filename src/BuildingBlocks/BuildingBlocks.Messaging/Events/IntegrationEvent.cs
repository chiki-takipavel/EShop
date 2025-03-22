using NodaTime;

namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public Instant OcurredOn { get; } = SystemClock.Instance.GetCurrentInstant();
    public string EventType => GetType().AssemblyQualifiedName!;
}
