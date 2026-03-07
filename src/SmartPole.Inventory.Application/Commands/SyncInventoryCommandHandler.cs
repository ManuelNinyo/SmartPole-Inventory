using MediatR;
using SmartPole.Inventory.Domain.Entities;
using SmartPole.Inventory.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SmartPole.Inventory.Application.Commands;

public class SyncInventoryCommandHandler : IRequestHandler<SyncInventoryCommand, bool> {
    private readonly IApplicationDbContext _context;
    private readonly ILogger<SyncInventoryCommandHandler> _logger;

    public SyncInventoryCommandHandler(IApplicationDbContext context, ILogger<SyncInventoryCommandHandler> logger) {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(SyncInventoryCommand request, CancellationToken cancellationToken) {
        if (request.Items == null || !request.Items.Any()) return true;

        foreach (var itemDto in request.Items) {
            // Verificar si el inventario ya existe para no duplicar en sincronizaciones repetidas
            var existingItem = await _context.InventoryItems.FindAsync(new object[] { itemDto.Id }, cancellationToken);

            if (existingItem == null) {
                var newItem = new InventoryItem(
                    itemDto.Id,
                    itemDto.SmartPoleId,
                    itemDto.ItemType,
                    itemDto.Confidence,
                    itemDto.DetectedAt,
                    itemDto.ImagePath
                );

                _context.InventoryItems.Add(newItem);
                _logger.LogInformation("Added new InventoryItem {ItemId} to SmartPole {SmartPoleId}", itemDto.Id, itemDto.SmartPoleId);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
