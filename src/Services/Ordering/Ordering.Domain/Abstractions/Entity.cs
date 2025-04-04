namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; } = default!;
    public Instant? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public Instant? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
