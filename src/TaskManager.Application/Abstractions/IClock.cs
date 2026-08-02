namespace TaskManager.Application.Abstractions;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
