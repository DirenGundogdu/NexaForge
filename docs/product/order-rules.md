# Draft Order Rules

## Definitions

- Product: An item offered for sale.
- SKU: A stock-keeping code that is unique within the wholesale business.
- Order: A business customer's request to purchase products.
- Order Line: A product and its requested quantity within an order.
- Draft: An order that has not yet been submitted.

## Business Rules

- ORD-001: A new order must have the Draft status.
- ORD-002: An order must belong to a business customer.
- ORD-003: A draft order may contain no lines.
- ORD-004: Each order line must have a positive integer quantity.
- ORD-005: A product may appear only once in an order.
- ORD-006: Creating a draft order must not reserve or decrease inventory.

## Acceptance Scenarios

Scenario: Create an empty draft order
    Given a business customer exists
    When the customer creates a draft order with no lines
    Then the draft order is created successfully
    And its status is Draft
    And it contains no lines

Scenario: Reject draft order creation with a zero quantity
    Given a business customer exists
    And a product exists
    When the customer creates a draft order with a quantity of 0 for that product
    Then the request is rejected
    And no order is saved
    And a validation error indicates that quantity must be a positive integer

Scenario: Create a draft order with a quantity exceeding available stock
    Given a business customer exists
    And a product has 5 units of available stock
    When the customer creates a draft order with 10 units of that product
    Then the draft order is created successfully
    And its status is Draft
    And the order line quantity is 10
    And no inventory is reserved
    And the available stock remains 5
