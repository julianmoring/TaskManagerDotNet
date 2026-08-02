using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests;

public sealed class CardSpecTests
{
    [Fact]
    public void Create_SetsAllProperties()
    {
        var spec = CardSpec.Create(cardId: 1, version: 1, "# Title", Clock.CreatedAt);

        Assert.Equal(1L, spec.CardId);
        Assert.Equal(1, spec.Version);
        Assert.Equal("# Title", spec.BodyMarkdown);
        Assert.Equal(Clock.CreatedAt, spec.CreatedAt);
        Assert.Equal(0L, spec.Id);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Create_WithNonPositiveCardId_Throws(long cardId)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CardSpec.Create(cardId, 1, "body", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Create_WithVersionLessThanOne_Throws(int version)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CardSpec.Create(1, version, "body", Clock.CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidBody_Throws(string? body)
    {
        Assert.Throws<ArgumentException>(() => CardSpec.Create(1, 1, body!, Clock.CreatedAt));
    }
}
