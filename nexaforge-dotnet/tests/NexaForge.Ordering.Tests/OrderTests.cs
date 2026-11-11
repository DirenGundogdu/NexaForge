
namespace NexaForge.Ordering.Tests;

public sealed class OrderTests
{
    [Fact]
    public void Constructor_WithCustomerId_CreatesEmptyDraft()
    {
        var customerId = Guid.NewGuid();

        var order = new Order(customerId);

        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.Equal(customerId, order.CustomerId);
        Assert.Equal(OrderStatus.Draft, order.Status);
        Assert.Empty(order.OrderLines);
    }

    [Fact]
    public void Constructor_WithEmptyCustomerId_Throws()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Order(Guid.Empty));

        Assert.Equal("customerId", exception.ParamName);
    }

    [Fact]
public void AddLine_WithDifferentProducts_AddsBothLines()
{
    var order = new Order(Guid.NewGuid());
    var keyboardId = Guid.NewGuid();
    var monitorId = Guid.NewGuid();

    order.AddLine(keyboardId, 10);
    order.AddLine(monitorId, 5);

    Assert.Equal(2, order.OrderLines.Count);
    Assert.Contains(order.OrderLines,
        line => line.ProductId == keyboardId && line.Quantity == 10);
    Assert.Contains(order.OrderLines,
        line => line.ProductId == monitorId && line.Quantity == 5);
}

[Fact]
public void AddLine_WithDuplicateProduct_RejectsSecondLine()
{
    var order = new Order(Guid.NewGuid());
    var productId = Guid.NewGuid();
    order.AddLine(productId, 10);

    Assert.Throws<InvalidOperationException>(
        () => order.AddLine(productId, 3));

    var remainingLine = Assert.Single(order.OrderLines);
    Assert.Equal(productId, remainingLine.ProductId);
    Assert.Equal(10, remainingLine.Quantity);
}
}