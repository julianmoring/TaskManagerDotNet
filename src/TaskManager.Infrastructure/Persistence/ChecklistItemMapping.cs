using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class ChecklistItemMapping : EntityMapping<ChecklistItem>
{
    public override void Configure(EntityTypeBuilder<ChecklistItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("ChecklistItem");

        builder.Property(e => e.ChecklistId).IsRequired();
        builder.Property(e => e.Text).IsRequired().HasMaxLength(500);
        builder.Property(e => e.IsDone).IsRequired();
        builder.Property(e => e.Position).IsRequired();

        builder.HasIndex(e => new { e.ChecklistId, e.Position });
    }
}
