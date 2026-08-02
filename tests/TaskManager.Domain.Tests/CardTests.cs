using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests;

public sealed class CardTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var card = Card.Create(columnId: 1, "Task", position: 0, Clock.CreatedAt);

        Assert.Equal(1L, card.ColumnId);
        Assert.Equal("Task", card.Title);
        Assert.Equal(0, card.Position);
        Assert.Equal(Clock.CreatedAt, card.CreatedAt);
        Assert.Equal(0L, card.Id);
    }

    [Fact]
    public void Create_InitializesDefaultPriorityAndEmptySubCollections()
    {
        var card = Card.Create(1, "Task", 0, Clock.CreatedAt);

        Assert.Equal(Priority.None, card.Priority);
        Assert.Null(card.DueDate);
        Assert.Null(card.Description);
        Assert.Null(card.UpdatedAt);
        Assert.Empty(card.Checklists);
        Assert.Empty(card.Comments);
        Assert.Empty(card.Labels);
        Assert.Empty(card.Specs);
        Assert.Empty(card.Sessions);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveColumnId_Throws(long columnId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Card.Create(columnId, "Task", 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidTitle_Throws(string? title)
    {
        Assert.Throws<ArgumentException>(() => Card.Create(1, title!, 0, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-50)]
    public void Create_WithNegativePosition_Throws(int position)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Card.Create(1, "Task", position, Clock.CreatedAt));
    }

    [Fact]
    public void Update_SetsTitleDescriptionPriorityDueDateAndUpdatedAt()
    {
        var card = Card.Create(1, "Old", 0, Clock.CreatedAt);
        var due = new DateTimeOffset(2026, 12, 31, 0, 0, 0, TimeSpan.Zero);

        card.Update("New", "desc", Priority.High, due, Clock.UpdatedAt);

        Assert.Equal("New", card.Title);
        Assert.Equal("desc", card.Description);
        Assert.Equal(Priority.High, card.Priority);
        Assert.Equal(due, card.DueDate);
        Assert.Equal(Clock.UpdatedAt, card.UpdatedAt);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Update_WithInvalidTitle_Throws(string? title)
    {
        var card = Card.Create(1, "Old", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentException>(() => card.Update(title!, "d", Priority.High, null, Clock.UpdatedAt));
    }

    [Fact]
    public void MoveTo_UpdatesColumnIdPositionAndUpdatedAt()
    {
        var card = Card.Create(1, "Task", 0, Clock.CreatedAt);

        card.MoveTo(columnId: 2, position: 3, Clock.UpdatedAt);

        Assert.Equal(2L, card.ColumnId);
        Assert.Equal(3, card.Position);
        Assert.Equal(Clock.UpdatedAt, card.UpdatedAt);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-7L)]
    public void MoveTo_WithNonPositiveColumnId_Throws(long columnId)
    {
        var card = Card.Create(1, "Task", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => card.MoveTo(columnId, 0, Clock.UpdatedAt));
    }

    [Fact]
    public void MoveTo_WithNegativePosition_Throws()
    {
        var card = Card.Create(1, "Task", 0, Clock.CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => card.MoveTo(2, -1, Clock.UpdatedAt));
    }
}
