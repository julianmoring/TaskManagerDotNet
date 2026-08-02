namespace TaskManager.Domain.Entities;

public sealed class ChecklistItem : Entity
{
    internal ChecklistItem()
    {
    }

    private ChecklistItem(long checklistId, string text, int position, DateTimeOffset createdAt)
    {
        ChecklistId = checklistId;
        Text = text;
        Position = position;
        CreatedAt = createdAt;
    }

    public long ChecklistId { get; private set; }

    public string Text { get; private set; } = string.Empty;

    public bool IsDone { get; private set; }

    public int Position { get; private set; }

    public static ChecklistItem Create(long checklistId, string text, int position, DateTimeOffset createdAt)
    {
        if (checklistId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(checklistId), "ChecklistId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Item text is required.", nameof(text));
        }

        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");
        }

        return new ChecklistItem(checklistId, text.Trim(), position, createdAt);
    }

    public void Toggle()
    {
        IsDone = !IsDone;
    }

    public void Update(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Item text is required.", nameof(text));
        }

        Text = text.Trim();
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
