using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public sealed class SessionEvent : Entity
{
    internal SessionEvent()
    {
    }

    private SessionEvent(long sessionId, EventKind kind, string text, DateTimeOffset createdAt)
    {
        SessionId = sessionId;
        Kind = kind;
        Text = text ?? string.Empty;
        CreatedAt = createdAt;
    }

    public long SessionId { get; private set; }

    public EventKind Kind { get; private set; }

    public string Text { get; private set; } = string.Empty;

    public static SessionEvent Create(long sessionId, EventKind kind, string text, DateTimeOffset createdAt)
    {
        if (sessionId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sessionId), "SessionId must be positive.");
        }

        if (text is null)
        {
            throw new ArgumentNullException(nameof(text), "Event text cannot be null.");
        }

        return new SessionEvent(sessionId, kind, text, createdAt);
    }
}
