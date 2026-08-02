using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class CardLabelTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var link = CardLabel.Create(cardId: 1, labelId: 2, Clock.CreatedAt);

        Assert.Equal(1L, link.CardId);
        Assert.Equal(2L, link.LabelId);
        Assert.Equal(Clock.CreatedAt, link.CreatedAt);
        Assert.Equal(0L, link.Id);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-4L)]
    public void Create_WithNonPositiveCardId_Throws(long cardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CardLabel.Create(cardId, 1, Clock.CreatedAt));
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-5L)]
    public void Create_WithNonPositiveLabelId_Throws(long labelId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CardLabel.Create(1, labelId, Clock.CreatedAt));
    }
}
