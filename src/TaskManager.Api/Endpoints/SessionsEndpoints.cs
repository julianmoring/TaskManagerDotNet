using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Sessions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Sessions;
using TaskManager.Infrastructure.Sse;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class SessionsEndpoints
{
    [WolverinePost("/api/cards/{cardId}/sessions")]
    public static Task<StartSessionResponse> Start(
        long cardId,
        StartSessionCommand command,
        ICardRepository cardRepository,
        ICardSpecRepository cardSpecRepository,
        IOpenCodeSessionRepository sessionRepository,
        IOpenCodeHost openCodeHost,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => StartSessionHandler.HandleAsync(command with { CardId = cardId }, cardRepository, cardSpecRepository, sessionRepository, openCodeHost, unitOfWork, clock, cancellationToken);

    [WolverinePost("/api/sessions/{sessionId}/stop")]
    public static Task Stop(
        long sessionId,
        IOpenCodeSessionRepository sessionRepository,
        IOpenCodeHost openCodeHost,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => StopSessionHandler.HandleAsync(new StopSessionCommand(sessionId), sessionRepository, openCodeHost, unitOfWork, clock, cancellationToken);

    [WolverineGet("/api/sessions/{sessionId}")]
    public static Task<OpenCodeSessionDto?> Get(
        long sessionId,
        IOpenCodeSessionRepository sessionRepository,
        CancellationToken cancellationToken)
        => GetSessionHandler.HandleAsync(new GetSessionQuery(sessionId), sessionRepository, cancellationToken);

    [WolverineGet("/api/cards/{cardId}/sessions")]
    public static Task<IReadOnlyList<OpenCodeSessionDto>> ListByCard(
        long cardId,
        IOpenCodeSessionRepository sessionRepository,
        CancellationToken cancellationToken)
        => ListSessionsByCardHandler.HandleAsync(new ListSessionsByCardQuery(cardId), sessionRepository, cancellationToken);

    [WolverineGet("/api/sessions/{sessionId}/events")]
    public static Task<IReadOnlyList<SessionEventDto>> ListEvents(
        long sessionId,
        long? since,
        ISessionEventRepository sessionEventRepository,
        CancellationToken cancellationToken)
        => ListSessionEventsHandler.HandleAsync(new ListSessionEventsQuery(sessionId, since), sessionEventRepository, cancellationToken);

    [WolverineGet("/api/sessions/{sessionId}/events/stream")]
    public static IResult Stream(
        long sessionId,
        ISessionEventChannel channel,
        CancellationToken cancellationToken)
    {
        return Results.Stream(async stream =>
        {
            await using var writer = new StreamWriter(stream);
            await foreach (var evt in channel.SubscribeAsync(sessionId, cancellationToken))
            {
                var json = System.Text.Json.JsonSerializer.Serialize(evt);
                await writer.WriteAsync($"data: {json}\n\n".AsMemory(), cancellationToken);
                await writer.FlushAsync();
            }
        }, contentType: "text/event-stream");
    }
}
