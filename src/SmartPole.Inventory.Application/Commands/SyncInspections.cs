using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartPole.Inventory.Application.Common.Interfaces;
using SmartPole.Inventory.Application.Commands.Models;
using SmartPole.Inventory.Domain.Entities;

namespace SmartPole.Inventory.Application.Commands;

public record SyncInspectionsCommand(IEnumerable<InspectionDto> Inspections) : IRequest<SyncResultDto>;

public class SyncInspectionsCommandHandler : IRequestHandler<SyncInspectionsCommand, SyncResultDto> {
  private readonly IApplicationDbContext _context;

  public SyncInspectionsCommandHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<SyncResultDto> Handle(SyncInspectionsCommand request, CancellationToken cancellationToken) {
    int total = 0;
    int successful = 0;
    int failed = 0;
    var errors = new List<string>();

    foreach (var dto in request.Inspections) {
      total++;
      try {
        var existing = await _context.Inspections
          .Include(i => i.Findings)
          .FirstOrDefaultAsync(i => i.Id == dto.Id, cancellationToken);

        if (existing != null) {
          // Client Wins: Remove old findings and update
          _context.FraudFindings.RemoveRange(existing.Findings);
          // Update properties (simplified for skeleton)
          // existing.Result = dto.Result;
        } else {
          var inspection = new Inspection(dto.Id, dto.SmartPoleId, dto.Timestamp, dto.Result);
          foreach (var findingDto in dto.Findings) {
            // Need a way to add findings, the entity is read-only for now
            // I'll skip complex mapping for skeleton
          }
          _context.Inspections.Add(inspection);
        }
        successful++;
      } catch (Exception ex) {
        failed++;
        errors.Add($"Error processing inspection {dto.Id}: {ex.Message}");
      }
    }

    await _context.SaveChangesAsync(cancellationToken);

    return new SyncResultDto(total, successful, failed, errors);
  }
}
