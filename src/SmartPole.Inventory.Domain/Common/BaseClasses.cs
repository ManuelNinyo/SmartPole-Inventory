namespace SmartPole.Inventory.Domain.Common;

public abstract class Entity<TId> {
  public TId Id { get; protected set; }

  protected Entity(TId id) {
    Id = id;
  }

  public override bool Equals(object? obj) {
    if (obj is not Entity<TId> other) return false;
    if (ReferenceEquals(this, other)) return true;
    if (Id is null || other.Id is null) return false;
    return Id.Equals(other.Id);
  }

  public override int GetHashCode() {
    return Id?.GetHashCode() ?? 0;
  }

  public static bool operator ==(Entity<TId>? a, Entity<TId>? b) {
    if (a is null && b is null) return true;
    if (a is null || b is null) return false;
    return a.Equals(b);
  }

  public static bool operator !=(Entity<TId>? a, Entity<TId>? b) {
    return !(a == b);
  }
}

public abstract class ValueObject {
  protected abstract IEnumerable<object> GetEqualityComponents();

  public override bool Equals(object? obj) {
    if (obj is null || obj.GetType() != GetType()) return false;
    var other = (ValueObject)obj;
    return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
  }

  public override int GetHashCode() {
    return GetEqualityComponents()
      .Select(x => x?.GetHashCode() ?? 0)
      .Aggregate((x, y) => x ^ y);
  }

  public static bool operator ==(ValueObject? a, ValueObject? b) {
    if (a is null && b is null) return true;
    if (a is null || b is null) return false;
    return a.Equals(b);
  }

  public static bool operator !=(ValueObject? a, ValueObject? b) {
    return !(a == b);
  }
}

public interface IAggregateRoot { }

public abstract class DomainEvent {
  public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public abstract class AuditableEntity<TId> : Entity<TId> {
  public DateTime CreatedAt { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public string? UpdatedBy { get; set; }

  protected AuditableEntity(TId id) : base(id) { }
}
