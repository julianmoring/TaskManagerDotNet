namespace TaskManager.Domain.Entities;

public sealed class CardSpec : Entity
{
    internal CardSpec()
    {
    }

    private CardSpec(long cardId, int version, string bodyMarkdown, DateTimeOffset createdAt)
    {
        CardId = cardId;
        Version = version;
        BodyMarkdown = bodyMarkdown;
        CreatedAt = createdAt;
    }

    public long CardId { get; private set; }

    public int Version { get; private set; }

    public string BodyMarkdown { get; private set; } = string.Empty;

    public static CardSpec Create(long cardId, int version, string bodyMarkdown, DateTimeOffset createdAt)
    {
        if (cardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cardId), "CardId must be positive.");
        }

        if (version < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(version), "Version must be 1 or greater.");
        }

        if (string.IsNullOrWhiteSpace(bodyMarkdown))
        {
            throw new ArgumentException("Spec body is required.", nameof(bodyMarkdown));
        }

        return new CardSpec(cardId, version, bodyMarkdown, createdAt);
    }
}
