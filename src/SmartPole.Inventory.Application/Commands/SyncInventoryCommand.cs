using MediatR;
using System;
using System.Collections.Generic;

namespace SmartPole.Inventory.Application.Commands;

public class SyncInventoryItemDto {
    public Guid Id { get; set; }
    public Guid SmartPoleId { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public float Confidence { get; set; }
    public DateTime DetectedAt { get; set; }
    public string? ImagePath { get; set; }
}

public class SyncInventoryCommand : IRequest<bool> {
    public List<SyncInventoryItemDto> Items { get; set; } = new();
}
