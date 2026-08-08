using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Checklists;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Checklists;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class ChecklistsEndpoints
{
    [WolverinePost("/api/cards/{cardId}/checklists")]
    public static Task<CreateChecklistResponse> Create(
        long cardId,
        CreateChecklistCommand command,
        ICardRepository cardRepository,
        IChecklistRepository checklistRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => CreateChecklistHandler.HandleAsync(command with { CardId = cardId }, cardRepository, checklistRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/checklists/{checklistId}/name")]
    public static Task Rename(
        long checklistId,
        RenameChecklistCommand command,
        IChecklistRepository checklistRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => RenameChecklistHandler.HandleAsync(command with { ChecklistId = checklistId }, checklistRepository, unitOfWork, cancellationToken);

    [WolverineDelete("/api/checklists/{checklistId}")]
    public static Task Delete(
        long checklistId,
        IChecklistRepository checklistRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DeleteChecklistHandler.HandleAsync(new DeleteChecklistCommand(checklistId), checklistRepository, unitOfWork, cancellationToken);

    [WolverinePost("/api/checklists/{checklistId}/items")]
    public static Task AddItem(
        long checklistId,
        AddChecklistItemCommand command,
        IChecklistRepository checklistRepository,
        IChecklistItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => AddChecklistItemHandler.HandleAsync(command with { ChecklistId = checklistId }, checklistRepository, itemRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/checklist-items/{itemId}")]
    public static Task UpdateItem(
        long itemId,
        UpdateChecklistItemCommand command,
        IChecklistItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => UpdateChecklistItemHandler.HandleAsync(command with { ItemId = itemId }, itemRepository, unitOfWork, cancellationToken);

    [WolverinePut("/api/checklist-items/{itemId}/done")]
    public static Task ToggleItem(
        long itemId,
        IChecklistItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => ToggleChecklistItemHandler.HandleAsync(new ToggleChecklistItemCommand(itemId), itemRepository, unitOfWork, cancellationToken);

    [WolverinePut("/api/checklist-items/{itemId}/position")]
    public static Task MoveItem(
        long itemId,
        MoveChecklistItemCommand command,
        IChecklistItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => MoveChecklistItemHandler.HandleAsync(command with { ItemId = itemId }, itemRepository, unitOfWork, cancellationToken);

    [WolverineDelete("/api/checklist-items/{itemId}")]
    public static Task DeleteItem(
        long itemId,
        IChecklistItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DeleteChecklistItemHandler.HandleAsync(new DeleteChecklistItemCommand(itemId), itemRepository, unitOfWork, cancellationToken);

    [WolverineGet("/api/cards/{cardId}/checklists")]
    public static Task<IReadOnlyList<ChecklistDto>> ListByCard(
        long cardId,
        IChecklistRepository checklistRepository,
        CancellationToken cancellationToken)
        => ListChecklistsByCardHandler.HandleAsync(new ListChecklistsByCardQuery(cardId), checklistRepository, cancellationToken);
}
