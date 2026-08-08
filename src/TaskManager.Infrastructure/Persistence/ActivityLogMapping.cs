using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class ActivityLogMapping : EntityMapping<ActivityLog>
{
    public override void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        base.Configure(builder);

        builder.ToTable("ActivityLog");

        builder.Property(e => e.BoardId).IsRequired();
        builder.Property(e => e.CardId).IsRequired(false);
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.Message).IsRequired().HasMaxLength(500);

        builder.HasIndex(e => new { e.BoardId, e.CreatedAt });

        builder.HasOne<Board>()
            .WithMany()
            .HasForeignKey(e => e.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Card>()
            .WithMany()
            .HasForeignKey(e => e.CardId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
