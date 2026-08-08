using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class OpenCodeSessionMapping : EntityMapping<OpenCodeSession>
{
    public override void Configure(EntityTypeBuilder<OpenCodeSession> builder)
    {
        base.Configure(builder);

        builder.ToTable("OpenCodeSession");
        builder.UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(e => e.CardId).IsRequired();
        builder.Property(e => e.SpecVersion).IsRequired();
        builder.Property(e => e.SpecBodySnapshot).IsRequired();
        builder.Property(e => e.WorkspacePath).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.Pid).IsRequired(false);
        builder.Property(e => e.StartedAt).IsRequired(false);
        builder.Property(e => e.EndedAt).IsRequired(false);
        builder.Property(e => e.ExitCode).IsRequired(false);

        builder.HasIndex(e => e.CardId);

        builder.HasMany(e => e.Events)
            .WithOne()
            .HasForeignKey(ev => ev.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
