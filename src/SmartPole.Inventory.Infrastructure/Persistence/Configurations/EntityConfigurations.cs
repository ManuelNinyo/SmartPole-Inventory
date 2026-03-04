using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPole.Inventory.Domain.Entities;

namespace SmartPole.Inventory.Infrastructure.Persistence.Configurations;

public class SmartPoleConfiguration : IEntityTypeConfiguration<SmartPole.Inventory.Domain.Entities.SmartPole> {
  public void Configure(EntityTypeBuilder<SmartPole.Inventory.Domain.Entities.SmartPole> builder) {
    builder.ToTable("SmartPoles");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Location)
      .HasColumnType("geometry(Point, 3857)")
      .IsRequired();

    builder.Property(x => x.Type).HasMaxLength(100).IsRequired();
    builder.Property(x => x.Status).HasMaxLength(50).IsRequired();

    // Audit properties
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.CreatedBy).HasMaxLength(200);
    builder.Property(x => x.UpdatedAt);
    builder.Property(x => x.UpdatedBy).HasMaxLength(200);

    builder.HasMany(x => x.Inspections)
      .WithOne(x => x.SmartPole)
      .HasForeignKey(x => x.SmartPoleId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}

public class InspectionConfiguration : IEntityTypeConfiguration<Inspection> {
  public void Configure(EntityTypeBuilder<Inspection> builder) {
    builder.ToTable("Inspections");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Timestamp).IsRequired();
    builder.Property(x => x.Result).HasMaxLength(500);

    // Audit properties
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.CreatedBy).HasMaxLength(200);
    builder.Property(x => x.UpdatedAt);
    builder.Property(x => x.UpdatedBy).HasMaxLength(200);

    builder.HasMany(x => x.Findings)
      .WithOne(x => x.Inspection)
      .HasForeignKey(x => x.InspectionId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}

public class FraudFindingConfiguration : IEntityTypeConfiguration<FraudFinding> {
  public void Configure(EntityTypeBuilder<FraudFinding> builder) {
    builder.ToTable("FraudFindings");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
    builder.Property(x => x.Severity).HasMaxLength(50).IsRequired();

    // Audit properties
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.CreatedBy).HasMaxLength(200);
    builder.Property(x => x.UpdatedAt);
    builder.Property(x => x.UpdatedBy).HasMaxLength(200);
  }
}

public class TelcoOperatorConfiguration : IEntityTypeConfiguration<TelcoOperator> {
  public void Configure(EntityTypeBuilder<TelcoOperator> builder) {
    builder.ToTable("TelcoOperators");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
    builder.Property(x => x.ContactInfo).HasMaxLength(500);

    // Audit properties
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.CreatedBy).HasMaxLength(200);
    builder.Property(x => x.UpdatedAt);
    builder.Property(x => x.UpdatedBy).HasMaxLength(200);
  }
}
