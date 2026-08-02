namespace TaskManager.Domain.Entities;

public sealed class Column : Entity
{
    private readonly List<Card> _cards = new();

    internal Column()
    {
    }

    private Column(long boardId, string name, int position, DateTimeOffset createdAt)
    {
        BoardId = boardId;
        Name = name;
        Position = position;
        CreatedAt = createdAt;
    }

    public long BoardId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Position { get; private set; }

    public IReadOnlyList<Card> Cards => _cards;

    public static Column Create(long boardId, string name, int position, DateTimeOffset createdAt)
    {
        if (boardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(boardId), "BoardId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Column name is required.", nameof(name));
        }

        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");
        }

        return new Column(boardId, name.Trim(), position, createdAt);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Column name is required.", nameof(name));
        }

        Name = name.Trim();
    }

    public void MoveTo(int position)
    {
        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");
        }

        Position = position;
    }
}
