namespace TaskManager.Application.Dtos;

public sealed record ChecklistDto(long Id, long CardId, string Title, IReadOnlyList<ChecklistItemDto> Items);

public sealed record ChecklistItemDto(long Id, long ChecklistId, string Text, bool IsDone, int Position);

public sealed record CreateChecklistResponse(long Id);
