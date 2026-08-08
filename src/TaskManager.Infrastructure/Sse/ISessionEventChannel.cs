using TaskManager.Application.Dtos;

namespace TaskManager.Infrastructure.Sse;

public interface ISessionEventChannel
{
    IAsyncEnumerable<SessionEventDto> SubscribeAsync(long sessionId, CancellationToken ct = default);

    void Publish(SessionEventDto @event);
}
