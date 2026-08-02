using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class SessionEventMapping : EntityMapping<SessionEvent>
{
    public override void Configure(EntityTypeBuilder<SessionEvent> builder)
    {
        base.Configure(builder);

        builder.ToTable("SessionEvent");

        builder.Property(e => e.SessionId).IsRequired();
        builder.Property(e => e.Kind).IsRequired();
        builder.Property(e => e.Text).IsRequired().HasMaxLength(4000);

        builder.HasIndex(e => new { e.SessionId, e.Id });
    }
}
