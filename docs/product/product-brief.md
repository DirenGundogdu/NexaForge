# NexaForge — B2B Order & Inventory Platform

## Problem

Wholesale businesses need a reliable way to accept business orders,
reserve inventory, and coordinate fulfillment without overselling
or processing the same order more than once.

## Primary Users

- Buyer
- Sales Operator
- Warehouse Operator
- Organization Administrator

## Core Workflow

1. A buyer creates a draft order.
2. The buyer submits the order.
3. The system reserves the required inventory.
4. Warehouse operators prepare and fulfill the order.
5. Cancellation releases inventory that is no longer required.

## Initial Scope

- One wholesale business
- Multiple business customers
- One warehouse
- One currency
- Draft order creation and retrieval

## Future Scope

- Inventory reservations
- Order submission and cancellation
- Partial fulfillment
- Multiple warehouses
- Multi-tenant operation
- Search and analytics
- AI-assisted product and operations support

## Out of Scope for the Initial Release

- Real payment processing
- Tax calculation
- Currency conversion
- Carrier integrations
- Microservices deployment