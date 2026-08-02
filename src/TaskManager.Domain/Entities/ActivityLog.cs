using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public sealed class ActivityLog : Entity
{
    internal ActivityLog()
    {
    }

    private ActivityLog(long boardId, long? cardId, ActivityType type, string message, DateTimeOffset createdAt)
    {
        BoardId = boardId;
        CardId = cardId;
        Type = type;
        Message = message;
        CreatedAt = createdAt;
    }

    public long? CardId { get; private set; }

    public long BoardId { get; private set; }

    public ActivityType Type { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public static ActivityLog Create(long boardId, long? cardId, ActivityType type, string message, DateTimeOffset createdAt)
    {
        if (boardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(boardId), "BoardId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Activity message is required.", nameof(message));
        }

        return new ActivityLog(boardId, cardId, type, message, createdAt);
    }
}
