using System.Collections.Concurrent;
using System.Threading.Channels;
using TaskManager.Application.Dtos;

namespace TaskManager.Infrastructure.Sse;

public sealed class SessionEventChannel : ISessionEventChannel
{
    private readonly ConcurrentDictionary<long, Channel<SessionEventDto>> _channels = new();

    public IAsyncEnumerable<SessionEventDto> SubscribeAsync(long sessionId, CancellationToken ct = default)
    {
        var ch = _channels.GetOrAdd(sessionId, _ => Channel.CreateUnbounded<SessionEventDto>());
        return ch.Reader.ReadAllAsync(ct);
    }

    public void Publish(SessionEventDto @event)
    {
        if (_channels.TryGetValue(@event.SessionId, out var ch))
        {
            ch.Writer.TryWrite(@event);
        }
    }
}
