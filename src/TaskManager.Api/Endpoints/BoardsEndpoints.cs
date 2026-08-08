using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Boards;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Boards;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class BoardsEndpoints
{
    [WolverinePost("/api/boards")]
    public static Task<CreateBoardResponse> Create(
        CreateBoardCommand command,
        IBoardRepository boardRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => CreateBoardHandler.HandleAsync(command, boardRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/boards/{boardId}/name")]
    public static Task Rename(
        long boardId,
        RenameBoardCommand command,
        IBoardRepository boardRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => RenameBoardHandler.HandleAsync(command with { BoardId = boardId }, boardRepository, unitOfWork, clock, cancellationToken);

    [WolverineDelete("/api/boards/{boardId}")]
    public static Task Delete(
        long boardId,
        IBoardRepository boardRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => DeleteBoardHandler.HandleAsync(new DeleteBoardCommand(boardId), boardRepository, unitOfWork, cancellationToken);

    [WolverineGet("/api/boards/{boardId}")]
    public static Task<BoardDto?> Get(
        long boardId,
        IBoardRepository boardRepository,
        CancellationToken cancellationToken)
        => GetBoardHandler.HandleAsync(new GetBoardQuery(boardId), boardRepository, cancellationToken);

    [WolverineGet("/api/boards")]
    public static Task<IReadOnlyList<BoardDto>> List(
        IBoardRepository boardRepository,
        CancellationToken cancellationToken)
        => ListBoardsHandler.HandleAsync(new ListBoardsQuery(), boardRepository, cancellationToken);
}
