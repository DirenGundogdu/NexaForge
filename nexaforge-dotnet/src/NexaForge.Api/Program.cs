using System.Collections.Concurrent;
using NexaForge.Api.Orders;
using NexaForge.Ordering;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var orders = new ConcurrentDictionary<Guid, DraftOrderResponse>();

// Development fixtures
var customerId = Guid.Parse("11111111-1111-4111-8111-111111111111");
var productId = Guid.Parse("33333333-3333-4333-8333-333333333333");

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/v1/orders", (CreateDraftOrderRequest request) =>
{
    if (request.CustomerId != customerId)
    {
        return Invalid("customerId", "UNKNOWN_CUSTOMER", "Unknown customer");
    }

    if (request.Lines is null)
    {
        return Invalid("lines", "INVALID_REQUEST", "Lines must be an array.");
    }

    var order = new Order(request.CustomerId);

    for (var i = 0; i < request.Lines.Length; i++)
    {
        var line = request.Lines[i];

        if (line is null)
            return Invalid($"lines[{i}]", "INVALID_REQUEST", "A line is required.");

        if (line.ProductId != productId)
            return Invalid($"lines[{i}].productId", "UNKNOWN_PRODUCT", "Unknown product.");

        try
        {
            order.AddLine(line.ProductId, line.Quantity);
        }
        catch (ArgumentOutOfRangeException)
        {
            return Invalid($"lines[{i}].quantity", "INVALID_QUANTITY",
                "Quantity must be greater than zero.");
        }
        catch (InvalidOperationException)
        {
            return Invalid($"lines[{i}].productId", "DUPLICATE_PRODUCT",
                "The product is already present in this order.");
        }
    }

    var response = new DraftOrderResponse(
           order.Id, order.CustomerId, order.Status.ToString(),
           order.OrderLines, DateTimeOffset.UtcNow);

    orders[order.Id] = response;
    return Results.Created($"/api/v1/orders/{order.Id}", response);
});

app.MapGet("/api/v1/orders/{orderId:guid}", (Guid orderId) =>
{
    if (orders.TryGetValue(orderId, out var order))
        return Results.Ok(order);

    return Results.NotFound(new { code = "ORDER_NOT_FOUND", message = "Order not found." });
});

app.Run();


static IResult Invalid(string field, string code, string message) => Results.BadRequest(new
{
    code = "VALIDATION_FAILED",
    message = "The request contains invalid values.",
    errors = new[] { new { field, code, message } }
});


internal sealed record DraftOrderResponse(Guid Id, Guid CustomerId, string Status, IReadOnlyList<OrderLine> Lines, DateTimeOffset CreatedAt);