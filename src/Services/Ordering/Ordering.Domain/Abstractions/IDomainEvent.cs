namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    public Guid EventId => Guid.NewGuid();
    public Instant OccuredOn => SystemClock.Instance.GetCurrentInstant();
    public string EventType => GetType().AssemblyQualifiedName!;
}
