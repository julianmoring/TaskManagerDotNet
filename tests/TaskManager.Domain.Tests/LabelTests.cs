using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class LabelTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var label = Label.Create(boardId: 1, "Bug", "#ff0000", Clock.CreatedAt);

        Assert.Equal(1L, label.BoardId);
        Assert.Equal("Bug", label.Name);
        Assert.Equal("#ff0000", label.Color);
        Assert.Equal(Clock.CreatedAt, label.CreatedAt);
        Assert.Equal(0L, label.Id);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveBoardId_Throws(long boardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Label.Create(boardId, "Bug", "#ff0000", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => Label.Create(1, name!, "#ff0000", Clock.CreatedAt));
    }

    [Theory]
    [InlineData("#fff")]
    [InlineData("ff0000")]
    [InlineData("#1234567")]
    [InlineData("red")]
    [InlineData("#abcdefg")]
    public void Create_WithInvalidColor_Throws(string color)
    {
        Assert.Throws<ArgumentException>(() => Label.Create(1, "Bug", color, Clock.CreatedAt));
    }

    [Fact]
    public void Create_WithUppercaseHexColor_Succeeds()
    {
        var label = Label.Create(1, "Bug", "#ABCDEF", Clock.CreatedAt);

        Assert.Equal("#ABCDEF", label.Color);
    }

    [Fact]
    public void Recolor_UpdatesColor()
    {
        var label = Label.Create(1, "Bug", "#ff0000", Clock.CreatedAt);

        label.Recolor("#aabbcc");

        Assert.Equal("#aabbcc", label.Color);
    }

    [Fact]
    public void Recolor_WithInvalidColor_Throws()
    {
        var label = Label.Create(1, "Bug", "#ff0000", Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => label.Recolor("invalid"));
    }

    [Fact]
    public void Rename_UpdatesName()
    {
        var label = Label.Create(1, "Bug", "#ff0000", Clock.CreatedAt);

        label.Rename("Feature");

        Assert.Equal("Feature", label.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Rename_WithInvalidName_Throws(string? name)
    {
        var label = Label.Create(1, "Bug", "#ff0000", Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => label.Rename(name!));
    }
}
