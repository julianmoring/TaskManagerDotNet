using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class CommentTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var comment = Comment.Create(cardId: 1, "body text", Clock.CreatedAt);

        Assert.Equal(1L, comment.CardId);
        Assert.Equal("body text", comment.Body);
        Assert.Equal(Clock.CreatedAt, comment.CreatedAt);
        Assert.Equal(0L, comment.Id);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-2L)]
    public void Create_WithNonPositiveCardId_Throws(long cardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Comment.Create(cardId, "body", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidBody_Throws(string? body)
    {
        Assert.Throws<ArgumentException>(() => Comment.Create(1, body!, Clock.CreatedAt));
    }
}
