using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class ChecklistMapping : EntityMapping<Checklist>
{
    public override void Configure(EntityTypeBuilder<Checklist> builder)
    {
        base.Configure(builder);

        builder.ToTable("Checklist");
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(e => e.CardId).IsRequired();
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);

        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(i => i.ChecklistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
