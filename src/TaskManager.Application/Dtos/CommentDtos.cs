namespace TaskManager.Application.Dtos;

public sealed record CommentDto(long Id, long CardId, string Body, DateTimeOffset CreatedAt);

public sealed record AddCommentResponse(long Id);
