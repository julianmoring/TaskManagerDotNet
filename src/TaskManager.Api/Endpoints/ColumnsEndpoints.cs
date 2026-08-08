using TaskManager.Application.Abstractions;
using TaskManager.Application.Commands.Columns;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries.Columns;
using Wolverine.Http;

namespace TaskManager.Api.Endpoints;

public static class ColumnsEndpoints
{
    [WolverinePost("/api/boards/{boardId}/columns")]
    public static Task<CreateColumnResponse> Add(
        long boardId,
        AddColumnCommand command,
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        IUnitOfWork unitOfWork,
        IClock clock,
        CancellationToken cancellationToken)
        => AddColumnHandler.HandleAsync(command with { BoardId = boardId }, boardRepository, columnRepository, unitOfWork, clock, cancellationToken);

    [WolverinePut("/api/boards/{boardId}/columns/{columnId}/name")]
    public static Task Rename(
        long boardId,
        long columnId,
        RenameColumnCommand command,
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => RenameColumnHandler.HandleAsync(command with { BoardId = boardId, ColumnId = columnId }, boardRepository, columnRepository, unitOfWork, cancellationToken);

    [WolverinePut("/api/boards/{boardId}/columns/{columnId}/position")]
    public static Task Move(
        long boardId,
        long columnId,
        MoveColumnCommand command,
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => MoveColumnHandler.HandleAsync(command with { BoardId = boardId, ColumnId = columnId }, boardRepository, columnRepository, unitOfWork, cancellationToken);

    [WolverineDelete("/api/boards/{boardId}/columns/{columnId}")]
    public static Task Remove(
        long boardId,
        long columnId,
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => RemoveColumnHandler.HandleAsync(new RemoveColumnCommand(boardId, columnId), boardRepository, columnRepository, unitOfWork, cancellationToken);

    [WolverineGet("/api/boards/{boardId}/columns")]
    public static Task<IReadOnlyList<ColumnDto>> List(
        long boardId,
        IColumnRepository columnRepository,
        CancellationToken cancellationToken)
        => ListColumnsHandler.HandleAsync(new ListColumnsQuery(boardId), columnRepository, cancellationToken);
}
