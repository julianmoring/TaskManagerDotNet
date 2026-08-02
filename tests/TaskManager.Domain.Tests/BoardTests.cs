using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class BoardTests
{
    [Fact]
    public void Create_SetsNameAndDescription()
    {
        var board = Board.Create("Work", "desc", Clock.CreatedAt);

        Assert.Equal("Work", board.Name);
        Assert.Equal("desc", board.Description);
    }

    [Fact]
    public void Create_SetsCreatedAtAndIdAndNullUpdatedAt()
    {
        var board = Board.Create("Work", "desc", Clock.CreatedAt);

        Assert.Equal(Clock.CreatedAt, board.CreatedAt);
        Assert.Equal(0L, board.Id);
        Assert.Null(board.UpdatedAt);
    }

    [Fact]
    public void Create_InitializesEmptyColumnsAndLabels()
    {
        var board = Board.Create("Work", "desc", Clock.CreatedAt);

        Assert.Empty(board.Columns);
        Assert.Empty(board.Labels);
    }

    [Fact]
    public void Create_TrimsName()
    {
        var board = Board.Create("  Work  ", "desc", Clock.CreatedAt);

        Assert.Equal("Work", board.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => Board.Create(name!, "desc", Clock.CreatedAt));
    }

    [Fact]
    public void Create_WithVeryLongName_Succeeds()
    {
        var longName = new string('a', 1000);

        var board = Board.Create(longName, null, Clock.CreatedAt);

        Assert.Equal(longName, board.Name);
    }

    [Fact]
    public void Rename_UpdatesNameAndUpdatedAt()
    {
        var board = Board.Create("Work", "desc", Clock.CreatedAt);

        board.Rename("Home", Clock.UpdatedAt);

        Assert.Equal("Home", board.Name);
        Assert.Equal(Clock.UpdatedAt, board.UpdatedAt);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Rename_WithInvalidName_Throws(string? name)
    {
        var board = Board.Create("Work", "desc", Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => board.Rename(name!, Clock.UpdatedAt));
    }

    [Fact]
    public void SetDescription_UpdatesDescriptionAndUpdatedAt()
    {
        var board = Board.Create("Work", "old", Clock.CreatedAt);

        board.SetDescription("new desc", Clock.UpdatedAt);

        Assert.Equal("new desc", board.Description);
        Assert.Equal(Clock.UpdatedAt, board.UpdatedAt);
    }

    [Fact]
    public void SetDescription_NullClearsDescription()
    {
        var board = Board.Create("Work", "old", Clock.CreatedAt);

        board.SetDescription(null, Clock.UpdatedAt);

        Assert.Null(board.Description);
    }
}
