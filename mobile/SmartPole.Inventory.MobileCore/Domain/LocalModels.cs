using SQLite;

namespace SmartPole.Inventory.MobileCore.Domain;

public class LocalPoste {
  [PrimaryKey]
  public Guid Id { get; set; }
  public string Location { get; set; }
  public string Type { get; set; }
  public string Status { get; set; }
  public SyncStatus SyncStatus { get; set; }
}

public class LocalInspeccion {
  [PrimaryKey, AutoIncrement]
  public int LocalId { get; set; }
  public Guid Id { get; set; } // Server ID if exists
  public Guid SmartPoleId { get; set; }
  public DateTime Timestamp { get; set; }
  public string Result { get; set; }
  public SyncStatus SyncStatus { get; set; }
}

public class LocalFraude {
  [PrimaryKey, AutoIncrement]
  public int LocalId { get; set; }
  public Guid Id { get; set; } // Server ID if exists
  public int InspectionLocalId { get; set; }
  public string Description { get; set; }
  public string Severity { get; set; }
  public SyncStatus SyncStatus { get; set; }
}
