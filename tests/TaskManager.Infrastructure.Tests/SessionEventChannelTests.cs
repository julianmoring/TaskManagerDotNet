using TaskManager.Application.Dtos;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Sse;

namespace TaskManager.Infrastructure.Tests;

public sealed class SessionEventChannelTests
{
    [Fact]
    public async Task Publish_to_subscribed_session_delivers_event()
    {
        var channel = new SessionEventChannel();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var received = new List<SessionEventDto>();
        var receivedTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var enumerable = channel.SubscribeAsync(42L, cts.Token);
        var consumer = Task.Run(async () =>
        {
            try
            {
                await foreach (var evt in enumerable)
                {
                    received.Add(evt);
                    receivedTcs.TrySetResult(true);
                    if (received.Count >= 1) break;
                }
            }
            catch (OperationCanceledException)
            {
            }
        });

        var evt = new SessionEventDto(1L, 42L, EventKind.StdOut, "hello", DateTimeOffset.UtcNow);
        channel.Publish(evt);

        await receivedTcs.Task.WaitAsync(TimeSpan.FromSeconds(5));
        cts.Cancel();
        await consumer;

        Assert.Single(received);
        Assert.Equal("hello", received[0].Text);
    }

    [Fact]
    public void Publish_with_no_subscribers_does_not_throw()
    {
        var channel = new SessionEventChannel();
        var evt = new SessionEventDto(1L, 999L, EventKind.StdOut, "hello", DateTimeOffset.UtcNow);

        var ex = Record.Exception(() => channel.Publish(evt));

        Assert.Null(ex);
    }

    [Fact]
    public async Task Multiple_subscribers_each_receive_events()
    {
        var channel = new SessionEventChannel();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var s1Received = new List<SessionEventDto>();
        var s2Received = new List<SessionEventDto>();
        var s1Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var s2Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var enumerable1 = channel.SubscribeAsync(1L, cts.Token);
        var enumerable2 = channel.SubscribeAsync(2L, cts.Token);
        var c1 = Task.Run(async () =>
        {
            try
            {
                await foreach (var evt in enumerable1)
                {
                    s1Received.Add(evt);
                    s1Tcs.TrySetResult(true);
                    if (s1Received.Count >= 1) break;
                }
            }
            catch (OperationCanceledException)
            {
            }
        });

        var c2 = Task.Run(async () =>
        {
            try
            {
                await foreach (var evt in enumerable2)
                {
                    s2Received.Add(evt);
                    s2Tcs.TrySetResult(true);
                    if (s2Received.Count >= 1) break;
                }
            }
            catch (OperationCanceledException)
            {
            }
        });

        channel.Publish(new SessionEventDto(1L, 1L, EventKind.StdOut, "s1", DateTimeOffset.UtcNow));
        channel.Publish(new SessionEventDto(1L, 2L, EventKind.StdOut, "s2", DateTimeOffset.UtcNow));

        await Task.WhenAll(
            s1Tcs.Task.WaitAsync(TimeSpan.FromSeconds(5)),
            s2Tcs.Task.WaitAsync(TimeSpan.FromSeconds(5)));
        cts.Cancel();
        await Task.WhenAll(c1, c2);

        Assert.Single(s1Received);
        Assert.Equal("s1", s1Received[0].Text);
        Assert.Single(s2Received);
        Assert.Equal("s2", s2Received[0].Text);
    }
}
