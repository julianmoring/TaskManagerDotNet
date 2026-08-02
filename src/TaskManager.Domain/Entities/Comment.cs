namespace TaskManager.Domain.Entities;

public sealed class Comment : Entity
{
    internal Comment()
    {
    }

    private Comment(long cardId, string body, DateTimeOffset createdAt)
    {
        CardId = cardId;
        Body = body;
        CreatedAt = createdAt;
    }

    public long CardId { get; private set; }

    public string Body { get; private set; } = string.Empty;

    public static Comment Create(long cardId, string body, DateTimeOffset createdAt)
    {
        if (cardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cardId), "CardId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Comment body is required.", nameof(body));
        }

        return new Comment(cardId, body, createdAt);
    }
}
