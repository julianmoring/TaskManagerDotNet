using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Labels;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Labels;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class LabelsEndpoints
{
    [WolverinePost("/api/boards/{boardId}/labels")]
    public static Task<CreateLabelResponse> Create(
        long boardId,
        CreateLabelCommand command,
        IBoardRepository boardRepository,
        ILabelRepository labelRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => CreateLabelHandler.HandleAsync(command with { BoardId = boardId }, boardRepository, labelRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/labels/{labelId}/name")]
    public static Task Rename(
        long labelId,
        RenameLabelCommand command,
        ILabelRepository labelRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => RenameLabelHandler.HandleAsync(command with { LabelId = labelId }, labelRepository, unitOfWork, cancellationToken);

    [WolverinePut("/api/labels/{labelId}/color")]
    public static Task Recolor(
        long labelId,
        RecolorLabelCommand command,
        ILabelRepository labelRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => RecolorLabelHandler.HandleAsync(command with { LabelId = labelId }, labelRepository, unitOfWork, cancellationToken);

    [WolverinePost("/api/cards/{cardId}/labels/{labelId}")]
    public static Task Attach(
        long cardId,
        long labelId,
        ICardRepository cardRepository,
        ILabelRepository labelRepository,
        ICardLabelRepository cardLabelRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => AttachLabelHandler.HandleAsync(new AttachLabelCommand(cardId, labelId), cardRepository, labelRepository, cardLabelRepository, unitOfWork, clock, cancellationToken);

    [WolverineDelete("/api/cards/{cardId}/labels/{labelId}")]
    public static Task Detach(
        long cardId,
        long labelId,
        ICardLabelRepository cardLabelRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DetachLabelHandler.HandleAsync(new DetachLabelCommand(cardId, labelId), cardLabelRepository, unitOfWork, cancellationToken);

    [WolverineGet("/api/boards/{boardId}/labels")]
    public static Task<IReadOnlyList<LabelDto>> ListByBoard(
        long boardId,
        ILabelRepository labelRepository,
        CancellationToken cancellationToken)
        => ListLabelsByBoardHandler.HandleAsync(new ListLabelsByBoardQuery(boardId), labelRepository, cancellationToken);
}
