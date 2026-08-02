using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public sealed class Card : Entity
{
    private readonly List<Checklist> _checklists = new();
    private readonly List<Comment> _comments = new();
    private readonly List<CardLabel> _labels = new();
    private readonly List<CardSpec> _specs = new();
    private readonly List<OpenCodeSession> _sessions = new();

    internal Card()
    {
    }

    private Card(long columnId, string title, int position, DateTimeOffset createdAt)
    {
        ColumnId = columnId;
        Title = title;
        Position = position;
        Priority = Priority.None;
        CreatedAt = createdAt;
    }

    public long ColumnId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public int Position { get; private set; }

    public Priority Priority { get; private set; }

    public DateTimeOffset? DueDate { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public IReadOnlyList<Checklist> Checklists => _checklists;

    public IReadOnlyList<Comment> Comments => _comments;

    public IReadOnlyList<CardLabel> Labels => _labels;

    public IReadOnlyList<CardSpec> Specs => _specs;

    public IReadOnlyList<OpenCodeSession> Sessions => _sessions;

    public static Card Create(long columnId, string title, int position, DateTimeOffset createdAt)
    {
        if (columnId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columnId), "ColumnId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Card title is required.", nameof(title));
        }

        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");
        }

        return new Card(columnId, title.Trim(), position, createdAt);
    }

    public void Update(string title, string? description, Priority priority, DateTimeOffset? dueDate, DateTimeOffset updatedAt)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Card title is required.", nameof(title));
        }

        Title = title.Trim();
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        UpdatedAt = updatedAt;
    }

    public void MoveTo(long columnId, int position, DateTimeOffset updatedAt)
    {
        if (columnId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columnId), "ColumnId must be positive.");
        }

        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");
        }

        ColumnId = columnId;
        Position = position;
        UpdatedAt = updatedAt;
    }
}
