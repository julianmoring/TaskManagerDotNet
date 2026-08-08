using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class LabelMapping : EntityMapping<Label>
{
    public override void Configure(EntityTypeBuilder<Label> builder)
    {
        base.Configure(builder);

        builder.ToTable("Label");

        builder.Property(e => e.BoardId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Color).IsRequired().HasMaxLength(7);

        builder.HasIndex(e => new { e.BoardId, e.Name }).IsUnique();
    }
}
