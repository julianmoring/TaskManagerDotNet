using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class ChecklistItemTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var item = ChecklistItem.Create(checklistId: 1, "Click button", 0, Clock.CreatedAt);

        Assert.Equal(1L, item.ChecklistId);
        Assert.Equal("Click button", item.Text);
        Assert.False(item.IsDone);
        Assert.Equal(0, item.Position);
        Assert.Equal(Clock.CreatedAt, item.CreatedAt);
        Assert.Equal(0L, item.Id);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-9L)]
    public void Create_WithNonPositiveChecklistId_Throws(long checklistId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ChecklistItem.Create(checklistId, "Click", 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidText_Throws(string? text)
    {
        Assert.Throws<ArgumentException>(() => ChecklistItem.Create(1, text!, 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-42)]
    public void Create_WithNegativePosition_Throws(int position)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ChecklistItem.Create(1, "Click", position, Clock.CreatedAt));
    }

    [Fact]
    public void Toggle_FlipsIsDoneTwiceBackToFalse()
    {
        var item = ChecklistItem.Create(1, "Click", 0, Clock.CreatedAt);

        item.Toggle();
        var afterFirst = item.IsDone;
        item.Toggle();
        var afterSecond = item.IsDone;

        Assert.True(afterFirst);
        Assert.False(afterSecond);
    }

    [Fact]
    public void Update_UpdatesText()
    {
        var item = ChecklistItem.Create(1, "Old", 0, Clock.CreatedAt);

        item.Update("New text");

        Assert.Equal("New text", item.Text);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Update_WithInvalidText_Throws(string? text)
    {
        var item = ChecklistItem.Create(1, "Old", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => item.Update(text!));
    }

    [Fact]
    public void MoveTo_UpdatesPosition()
    {
        var item = ChecklistItem.Create(1, "Click", 0, Clock.CreatedAt);

        item.MoveTo(2);

        Assert.Equal(2, item.Position);
    }

    [Fact]
    public void MoveTo_WithNegativePosition_Throws()
    {
        var item = ChecklistItem.Create(1, "Click", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => item.MoveTo(-1));
    }
}
