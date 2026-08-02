namespace TaskManager.Domain.Entities;

public sealed class Checklist : Entity
{
    private readonly List<ChecklistItem> _items = new();

    internal Checklist()
    {
    }

    private Checklist(long cardId, string title, DateTimeOffset createdAt)
    {
        CardId = cardId;
        Title = title;
        CreatedAt = createdAt;
    }

    public long CardId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public IReadOnlyList<ChecklistItem> Items => _items;

    public static Checklist Create(long cardId, string title, DateTimeOffset createdAt)
    {
        if (cardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cardId), "CardId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Checklist title is required.", nameof(title));
        }

        return new Checklist(cardId, title.Trim(), createdAt);
    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Checklist title is required.", nameof(title));
        }

        Title = title.Trim();
    }
}
