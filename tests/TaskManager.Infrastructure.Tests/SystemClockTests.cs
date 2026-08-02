using TaskManager.Infrastructure.Clock;

namespace TaskManager.Infrastructure.Tests;

public sealed class SystemClockTests
{
    [Fact]
    public void UtcNow_returns_value_close_to_now()
    {
        var before = DateTimeOffset.UtcNow.AddSeconds(-1).Ticks;
        var after = DateTimeOffset.UtcNow.AddSeconds(1).Ticks;

        var actual = new SystemClock().UtcNow;

        Assert.InRange(actual.Ticks, before, after);
    }
}
