namespace TaskManager.Infrastructure.Tests;

internal static class TestClock
{
    public static readonly DateTimeOffset CreatedAt = new(2026, 8, 2, 12, 0, 0, TimeSpan.Zero);

    public static readonly DateTimeOffset UpdatedAt = new(2026, 8, 2, 13, 0, 0, TimeSpan.Zero);
}
