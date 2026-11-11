using NexaForge.Ordering;

namespace NexaForge.Ordering.Ordering.Tests;

public sealed class OrderLineTests
{
   [Fact]
    public void Constructor_WithValidValues_CreatesOrderLine()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var line = new OrderLine(productId, 10);

        // Assert
        Assert.Equal(productId, line.ProductId);
        Assert.Equal(10, line.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithNonPositiveQuantity_Throws(int quantity)
    {
        var productId = Guid.NewGuid();

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new OrderLine(productId, quantity));

        Assert.Equal("quantity", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyProductId_Throws()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new OrderLine(Guid.Empty, 10));

        Assert.Equal("productId", exception.ParamName);
    }
}