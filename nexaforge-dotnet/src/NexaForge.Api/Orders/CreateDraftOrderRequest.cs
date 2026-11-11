namespace NexaForge.Api.Orders;

public sealed record CreateDraftOrderRequest()
{
   public required Guid CustomerId { get; init; }
   public required CreateOrderLineRequest[] Lines { get; init; }

}

public sealed record CreateOrderLineRequest()
{
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
}