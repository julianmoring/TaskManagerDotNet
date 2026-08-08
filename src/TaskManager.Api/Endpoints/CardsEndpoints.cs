using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Cards;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Cards;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class CardsEndpoints
{
    [WolverinePost("/api/columns/{columnId}/cards")]
    public static Task<CreateCardResponse> Create(
        long columnId,
        CreateCardCommand command,
        IColumnRepository columnRepository,
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => CreateCardHandler.HandleAsync(command with { ColumnId = columnId }, columnRepository, cardRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/cards/{cardId}")]
    public static Task Update(
        long cardId,
        UpdateCardCommand command,
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => UpdateCardHandler.HandleAsync(command with { CardId = cardId }, cardRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/cards/{cardId}/position")]
    public static Task Move(
        long cardId,
        MoveCardCommand command,
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => MoveCardHandler.HandleAsync(command with { CardId = cardId }, cardRepository, unitOfWork, clock, cancellationToken);

    [WolverineDelete("/api/cards/{cardId}")]
    public static Task Delete(
        long cardId,
        ICardRepository cardRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DeleteCardHandler.HandleAsync(new DeleteCardCommand(cardId), cardRepository, unitOfWork, cancellationToken);

    [WolverineGet("/api/cards/{cardId}")]
    public static Task<CardDto?> Get(
        long cardId,
        ICardRepository cardRepository,
        CancellationToken cancellationToken)
        => GetCardHandler.HandleAsync(new GetCardQuery(cardId), cardRepository, cancellationToken);

    [WolverineGet("/api/columns/{columnId}/cards")]
    public static Task<IReadOnlyList<CardDto>> ListByColumn(
        long columnId,
        ICardRepository cardRepository,
        CancellationToken cancellationToken)
        => ListCardsByColumnHandler.HandleAsync(new ListCardsByColumnQuery(columnId), cardRepository, cancellationToken);
}
