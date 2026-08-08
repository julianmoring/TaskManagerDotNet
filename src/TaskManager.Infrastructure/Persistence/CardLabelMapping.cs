using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class CardLabelMapping : EntityMapping<CardLabel>
{
    public override void Configure(EntityTypeBuilder<CardLabel> builder)
    {
        base.Configure(builder);

        builder.ToTable("CardLabel");

        builder.Property(e => e.CardId).IsRequired();
        builder.Property(e => e.LabelId).IsRequired();

        builder.HasIndex(e => new { e.CardId, e.LabelId }).IsUnique();

        builder.HasOne<Label>()
            .WithMany()
            .HasForeignKey(e => e.LabelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
