using SmartPole.Inventory.Domain.Common;
using NetTopologySuite.Geometries;

namespace SmartPole.Inventory.Domain.Entities;

public class SmartPole : AuditableEntity<Guid>, IAggregateRoot {
  public Point Location { get; private set; }
  public string Type { get; private set; }
  public string Status { get; private set; }

  // Navigation properties
  private readonly List<Inspection> _inspections = new();
  public IReadOnlyCollection<Inspection> Inspections => _inspections.AsReadOnly();
  private readonly List<InventoryItem> _inventoryItems = new();
  public IReadOnlyCollection<InventoryItem> InventoryItems => _inventoryItems.AsReadOnly();

  public SmartPole(Guid id, Point location, string type, string status) : base(id) {
    Location = location;
    Type = type;
    Status = status;
  }
}

public class Inspection : AuditableEntity<Guid> {
  public Guid SmartPoleId { get; private set; }
  public DateTime Timestamp { get; private set; }
  public string Result { get; private set; }

  // Navigation properties
  public SmartPole? SmartPole { get; private set; }
  private readonly List<FraudFinding> _findings = new();
  public IReadOnlyCollection<FraudFinding> Findings => _findings.AsReadOnly();

  public Inspection(Guid id, Guid smartPoleId, DateTime timestamp, string result) : base(id) {
    SmartPoleId = smartPoleId;
    Timestamp = timestamp;
    Result = result;
  }
}

public class FraudFinding : AuditableEntity<Guid> {
  public Guid InspectionId { get; private set; }
  public string Description { get; private set; }
  public string Severity { get; private set; }

  // Navigation properties
  public Inspection? Inspection { get; private set; }

  public FraudFinding(Guid id, Guid inspectionId, string description, string severity) : base(id) {
    InspectionId = inspectionId;
    Description = description;
    Severity = severity;
  }
}

public class TelcoOperator : AuditableEntity<Guid>, IAggregateRoot {
  public string Name { get; private set; }
  public string ContactInfo { get; private set; }

  public TelcoOperator(Guid id, string name, string contactInfo) : base(id) {
    Name = name;
    ContactInfo = contactInfo;
  }
}

public class User : AuditableEntity<Guid>, IAggregateRoot {
  public string Name { get; private set; }
  public string Role { get; private set; }

  public User(Guid id, string name, string role) : base(id) {
    Name = name;
    Role = role;
  }
}

public class InventoryItem : AuditableEntity<Guid> {
  public Guid SmartPoleId { get; private set; }
  public string ItemType { get; private set; }
  public float Confidence { get; private set; }
  public DateTime DetectedAt { get; private set; }
  public string? ImageUrl { get; private set; }

  // Navigation property
  public SmartPole? SmartPole { get; private set; }

  public InventoryItem(Guid id, Guid smartPoleId, string itemType, float confidence, DateTime detectedAt, string? imageUrl = null) : base(id) {
    SmartPoleId = smartPoleId;
    ItemType = itemType;
    Confidence = confidence;
    DetectedAt = detectedAt;
    ImageUrl = imageUrl;
  }
}
