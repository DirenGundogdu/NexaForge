namespace NexaForge.Ordering
{
    public sealed class OrderLine
    {
        public Guid ProductId { get; }

        public int Quantity { get; }

        public OrderLine(Guid productId, int quantity)
        {
            if(productId == Guid.Empty)
            {
                throw new ArgumentException("Product ID must not be empty.", nameof(productId));
            }

            if(quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity),  quantity, "Quantity must be greater than zero.");
            }

            ProductId = productId;
            Quantity = quantity;
        }
    }
}