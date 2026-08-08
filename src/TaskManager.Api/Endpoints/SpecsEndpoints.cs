using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Specs;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Specs;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class SpecsEndpoints
{
    [WolverinePost("/api/cards/{cardId}/specs")]
    public static Task<CreateSpecVersionResponse> CreateVersion(
        long cardId,
        CreateSpecVersionCommand command,
        ICardRepository cardRepository,
        ICardSpecRepository cardSpecRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => CreateSpecVersionHandler.HandleAsync(command with { CardId = cardId }, cardRepository, cardSpecRepository, unitOfWork, clock, cancellationToken);

    [WolverineGet("/api/cards/{cardId}/specs")]
    public static Task<IReadOnlyList<CardSpecSummaryDto>> ListVersions(
        long cardId,
        ICardSpecRepository cardSpecRepository,
        CancellationToken cancellationToken)
        => ListSpecVersionsHandler.HandleAsync(new ListSpecVersionsQuery(cardId), cardSpecRepository, cancellationToken);

    [WolverineGet("/api/cards/{cardId}/specs/active")]
    public static Task<CardSpecDto?> GetActive(
        long cardId,
        ICardSpecRepository cardSpecRepository,
        CancellationToken cancellationToken)
        => GetActiveSpecHandler.HandleAsync(new GetActiveSpecQuery(cardId), cardSpecRepository, cancellationToken);
}
