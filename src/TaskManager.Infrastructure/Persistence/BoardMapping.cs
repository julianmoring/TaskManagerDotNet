using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class BoardMapping : EntityMapping<Board>
{
    public override void Configure(EntityTypeBuilder<Board> builder)
    {
        base.Configure(builder);

        builder.ToTable("Board");
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).IsRequired(false);
        builder.Property(e => e.UpdatedAt).IsRequired(false);

        builder.HasIndex(e => e.Name);

        builder.HasMany(e => e.Columns)
            .WithOne()
            .HasForeignKey(c => c.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Labels)
            .WithOne()
            .HasForeignKey(l => l.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
