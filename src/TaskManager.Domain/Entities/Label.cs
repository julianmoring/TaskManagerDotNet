using System.Text.RegularExpressions;

namespace TaskManager.Domain.Entities;

public sealed class Label : Entity
{
    private static readonly Regex ColorRegex = new("^#[0-9a-fA-F]{6}$", RegexOptions.Compiled);

    internal Label()
    {
    }

    private Label(long boardId, string name, string color, DateTimeOffset createdAt)
    {
        BoardId = boardId;
        Name = name;
        Color = color;
        CreatedAt = createdAt;
    }

    public long BoardId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public static Label Create(long boardId, string name, string color, DateTimeOffset createdAt)
    {
        if (boardId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(boardId), "BoardId must be positive.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Label name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(color) || !ColorRegex.IsMatch(color))
        {
            throw new ArgumentException("Label color must be a hex string like '#aabbcc'.", nameof(color));
        }

        return new Label(boardId, name.Trim(), color, createdAt);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Label name is required.", nameof(name));
        }

        Name = name.Trim();
    }

    public void Recolor(string color)
    {
        if (string.IsNullOrWhiteSpace(color) || !ColorRegex.IsMatch(color))
        {
            throw new ArgumentException("Label color must be a hex string like '#aabbcc'.", nameof(color));
        }

        Color = color;
    }
}
