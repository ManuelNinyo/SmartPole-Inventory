using SmartPole.Inventory.Domain.Common;

namespace SmartPole.Inventory.Domain.Entities;

public class SmartPole : Entity<Guid>, IAggregateRoot {
  public string Location { get; private set; }
  public string Type { get; private set; }
  public string Status { get; private set; }

  public SmartPole(Guid id, string location, string type, string status) : base(id) {
    Location = location;
    Type = type;
    Status = status;
  }
}

public class MaintenanceRecord : Entity<Guid> {
  public DateTime Timestamp { get; private set; }
  public string Technician { get; private set; }
  public string Action { get; private set; }

  public MaintenanceRecord(Guid id, DateTime timestamp, string technician, string action) : base(id) {
    Timestamp = timestamp;
    Technician = technician;
    Action = action;
  }
}

public class User : Entity<Guid>, IAggregateRoot {
  public string Name { get; private set; }
  public string Role { get; private set; }

  public User(Guid id, string name, string role) : base(id) {
    Name = name;
    Role = role;
  }
}
