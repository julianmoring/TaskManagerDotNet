using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class CardSpecMapping : EntityMapping<CardSpec>
{
    public override void Configure(EntityTypeBuilder<CardSpec> builder)
    {
        base.Configure(builder);

        builder.ToTable("CardSpec");

        builder.Property(e => e.CardId).IsRequired();
        builder.Property(e => e.Version).IsRequired();
        builder.Property(e => e.BodyMarkdown).IsRequired();

        builder.HasIndex(e => new { e.CardId, e.Version }).IsUnique();
    }
}
