namespace TaskManager.Domain.Tests;

internal static class Clock
{
    public static readonly DateTimeOffset CreatedAt = new(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);

    public static readonly DateTimeOffset UpdatedAt = new(2026, 6, 15, 9, 30, 0, TimeSpan.Zero);
}
