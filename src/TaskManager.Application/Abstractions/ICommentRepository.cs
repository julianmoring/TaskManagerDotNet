using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstractions;

public interface ICommentRepository
{
    Task<IReadOnlyList<Comment>> ListByCardAsync(long cardId, CancellationToken ct = default);

    void Add(Comment comment);

    void Remove(Comment comment);
}
