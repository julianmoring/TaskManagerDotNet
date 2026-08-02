using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class ChecklistTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var checklist = Checklist.Create(cardId: 1, "Steps", Clock.CreatedAt);

        Assert.Equal(1L, checklist.CardId);
        Assert.Equal("Steps", checklist.Title);
        Assert.Equal(Clock.CreatedAt, checklist.CreatedAt);
        Assert.Equal(0L, checklist.Id);
        Assert.Empty(checklist.Items);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-3L)]
    public void Create_WithNonPositiveCardId_Throws(long cardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Checklist.Create(cardId, "Steps", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidTitle_Throws(string? title)
    {
        Assert.Throws<ArgumentException>(() => Checklist.Create(1, title!, Clock.CreatedAt));
    }

    [Fact]
    public void Rename_UpdatesTitle()
    {
        var checklist = Checklist.Create(1, "Old", Clock.CreatedAt);

        checklist.Rename("New");

        Assert.Equal("New", checklist.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    public void Rename_WithInvalidTitle_Throws(string? title)
    {
        var checklist = Checklist.Create(1, "Old", Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => checklist.Rename(title!));
    }
}
