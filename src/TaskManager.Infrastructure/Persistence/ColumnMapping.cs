using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class ColumnMapping : EntityMapping<Column>
{
    public override void Configure(EntityTypeBuilder<Column> builder)
    {
        base.Configure(builder);

        builder.ToTable("Column");
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(e => e.BoardId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Position).IsRequired();

        builder.HasIndex(e => new { e.BoardId, e.Position });

        builder.HasMany(e => e.Cards)
            .WithOne()
            .HasForeignKey(c => c.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
