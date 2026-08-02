namespace TaskManager.Domain.Entities;

public sealed class CardLabel : Entity
{
    internal CardLabel()
    {
    }

    private CardLabel(long cardId, long labelId, DateTimeOffset createdAt)
    {
        CardId = cardId;
        LabelId = labelId;
        CreatedAt = createdAt;
    }

    public long CardId { get; private set; }

    public long LabelId { get; private set; }

    public static CardLabel Create(long cardId, long labelId, DateTimeOffset createdAt)
    {
        if (cardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cardId), "CardId must be positive.");
        }

        if (labelId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(labelId), "LabelId must be positive.");
        }

        return new CardLabel(cardId, labelId, createdAt);
    }
}
