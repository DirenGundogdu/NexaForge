# Draft Order API Contract

## Create a Draft Order

- Method: POST
- Path: /api/v1/orders
- Success status: 201 Created
- The response includes a Location header.
- The server generates the order ID and creation timestamp.
- The initial order status is Draft.
- An empty lines array is allowed.
- Creating a draft order does not reserve inventory.

## Retrieve an Order

- Method: GET
- Path: /api/v1/orders/{orderId}
- Success status: 200 OK
- The response contains the stored order.
- A well-formed but unknown order ID returns 404 Not Found.

## Scope

- Authentication is deferred to a later milestone.
- Customer and product records are provided as development fixtures.
- Pricing and totals are deferred to a separate design decision.
- Validation error responses will be specified before implementation.