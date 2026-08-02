using TaskManager.Application.Abstractions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Specs;

public static class GetActiveSpecHandler
{
    public static Task<CardSpecDto?> HandleAsync(
        GetActiveSpecQuery query,
        ICardSpecRepository repo,
        CancellationToken ct)
    {
        return Task.FromResult<CardSpecDto?>(null);
    }
}
