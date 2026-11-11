namespace NexaForge.Ordering;

public enum OrderStatus
{
    Draft
}

public sealed class Order
{
    private readonly List<OrderLine> _orderLines = new List<OrderLine>();

    public Guid Id { get; }
    public Guid CustomerId { get; }
    public OrderStatus Status { get; }
    public IReadOnlyList<OrderLine> OrderLines { get; }


    public Order(Guid customerId)
    {
        if(customerId == Guid.Empty)
        {
            throw new ArgumentException("Customer ID must not be empty.", nameof(customerId));
        }
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Draft;
        OrderLines = _orderLines.AsReadOnly();
    }

    public void AddLine(Guid productId, int quantity)
    {
        var line = new OrderLine(productId, quantity);

        if(_orderLines.Any(existing => existing.ProductId == productId))
        {
            throw new InvalidOperationException("The product is already present in this order.");
        }

        _orderLines.Add(line);
    }
}