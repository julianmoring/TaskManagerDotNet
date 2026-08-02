namespace TaskManager.Domain.Entities;

public sealed class Board : Entity
{
    private readonly List<Column> _columns = new();
    private readonly List<Label> _labels = new();

    internal Board()
    {
    }

    private Board(string name, string? description, DateTimeOffset createdAt)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public IReadOnlyList<Column> Columns => _columns;

    public IReadOnlyList<Label> Labels => _labels;

    public static Board Create(string name, string? description, DateTimeOffset createdAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Board name is required.", nameof(name));
        }

        return new Board(name.Trim(), description, createdAt);
    }

    public void Rename(string name, DateTimeOffset updatedAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Board name is required.", nameof(name));
        }

        Name = name.Trim();
        UpdatedAt = updatedAt;
    }

    public void SetDescription(string? description, DateTimeOffset updatedAt)
    {
        Description = description;
        UpdatedAt = updatedAt;
    }
}
