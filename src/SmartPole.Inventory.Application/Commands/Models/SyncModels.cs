namespace SmartPole.Inventory.Application.Commands.Models;

public record InspectionDto(
  Guid Id,
  Guid SmartPoleId,
  DateTime Timestamp,
  string Result,
  IEnumerable<FraudFindingDto> Findings
);

public record FraudFindingDto(
  Guid Id,
  string Description,
  string Severity
);

public record SyncResultDto(
  int TotalProcessed,
  int Successful,
  int Failed,
  IEnumerable<string> Errors
);
