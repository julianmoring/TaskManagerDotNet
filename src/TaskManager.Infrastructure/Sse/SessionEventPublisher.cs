using TaskManager.Application.Dtos;

namespace TaskManager.Infrastructure.Sse;

public sealed class SessionEventPublisher
{
    private readonly ISessionEventChannel _channel;

    public SessionEventPublisher(ISessionEventChannel channel) => _channel = channel;

    public void Publish(SessionEventDto @event) => _channel.Publish(@event);
}
