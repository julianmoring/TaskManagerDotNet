namespace TaskManager.Application.Queries;

public sealed record GetSessionQuery(long SessionId);

public sealed record ListSessionsByCardQuery(long CardId);

public sealed record ListSessionEventsQuery(long SessionId, long? SinceEventId);
