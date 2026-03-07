using SQLite;
using System;

namespace SmartPole.Inventory.MobileCore.Models;

public class InventoryItemModel
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public Guid SmartPoleId { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public float Confidence { get; set; }
    public DateTime DetectedAt { get; set; }
    public string? ImagePath { get; set; }
    public bool IsSynced { get; set; }
}
