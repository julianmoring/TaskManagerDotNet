using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class ColumnTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var column = Column.Create(boardId: 1, "Todo", position: 0, Clock.CreatedAt);

        Assert.Equal(1L, column.BoardId);
        Assert.Equal("Todo", column.Name);
        Assert.Equal(0, column.Position);
        Assert.Equal(Clock.CreatedAt, column.CreatedAt);
        Assert.Equal(0L, column.Id);
        Assert.Empty(column.Cards);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveBoardId_Throws(long boardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Column.Create(boardId, "Todo", 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => Column.Create(1, name!, 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithNegativePosition_Throws(int position)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Column.Create(1, "Todo", position, Clock.CreatedAt));
    }

    [Fact]
    public void Rename_UpdatesName()
    {
        var column = Column.Create(1, "Todo", 0, Clock.CreatedAt);

        column.Rename("Backlog");

        Assert.Equal("Backlog", column.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Rename_WithInvalidName_Throws(string? name)
    {
        var column = Column.Create(1, "Todo", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => column.Rename(name!));
    }

    [Fact]
    public void MoveTo_UpdatesPosition()
    {
        var column = Column.Create(1, "Todo", 0, Clock.CreatedAt);

        column.MoveTo(3);

        Assert.Equal(3, column.Position);
    }

    [Fact]
    public void MoveTo_WithNegativePosition_Throws()
    {
        var column = Column.Create(1, "Todo", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => column.MoveTo(-1));
    }
}
