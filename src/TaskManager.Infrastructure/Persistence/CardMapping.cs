using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class CardMapping : EntityMapping<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.ToTable("Card");
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(e => e.ColumnId).IsRequired();
        builder.Property(e => e.Title).IsRequired().HasMaxLength(300);
        builder.Property(e => e.Description).IsRequired(false);
        builder.Property(e => e.Position).IsRequired();
        builder.Property(e => e.Priority).IsRequired();
        builder.Property(e => e.DueDate).IsRequired(false);
        builder.Property(e => e.UpdatedAt).IsRequired(false);

        builder.HasIndex(e => new { e.ColumnId, e.Position });

        builder.HasMany(e => e.Checklists)
            .WithOne()
            .HasForeignKey(c => c.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Comments)
            .WithOne()
            .HasForeignKey(c => c.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Labels)
            .WithOne()
            .HasForeignKey(cl => cl.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Specs)
            .WithOne()
            .HasForeignKey(s => s.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Sessions)
            .WithOne()
            .HasForeignKey(s => s.CardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
