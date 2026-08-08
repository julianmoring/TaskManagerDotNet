using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class CommentMapping : EntityMapping<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.ToTable("Comment");

        builder.Property(e => e.CardId).IsRequired();
        builder.Property(e => e.Body).IsRequired().HasMaxLength(4000);

        builder.HasIndex(e => e.CardId);
    }
}
