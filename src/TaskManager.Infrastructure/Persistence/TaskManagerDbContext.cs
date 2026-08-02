using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence;

public sealed class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Column> Columns => Set<Column>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<CardLabel> CardLabels => Set<CardLabel>();
    public DbSet<Checklist> Checklists => Set<Checklist>();
    public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<CardSpec> CardSpecs => Set<CardSpec>();
    public DbSet<OpenCodeSession> OpenCodeSessions => Set<OpenCodeSession>();
    public DbSet<SessionEvent> SessionEvents => Set<SessionEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntities(modelBuilder);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BoardMapping());
        modelBuilder.ApplyConfiguration(new ColumnMapping());
        modelBuilder.ApplyConfiguration(new CardMapping());
        modelBuilder.ApplyConfiguration(new LabelMapping());
        modelBuilder.ApplyConfiguration(new CardLabelMapping());
        modelBuilder.ApplyConfiguration(new ChecklistMapping());
        modelBuilder.ApplyConfiguration(new ChecklistItemMapping());
        modelBuilder.ApplyConfiguration(new CommentMapping());
        modelBuilder.ApplyConfiguration(new ActivityLogMapping());
        modelBuilder.ApplyConfiguration(new CardSpecMapping());
        modelBuilder.ApplyConfiguration(new OpenCodeSessionMapping());
        modelBuilder.ApplyConfiguration(new SessionEventMapping());
    }
}
