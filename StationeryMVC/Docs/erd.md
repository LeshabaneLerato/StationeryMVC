# StationeryMVC Entity Relationship Diagram (ERD)

## Entities and Relationships
- **User**: Id, Name, Email, Role
- **Item**: Id, Name, Category, Price, Stock
- **Quotation**: Id, UserId, Date, TotalAmount
- **QuotationItem**: Id, QuotationId, ItemId, Quantity, Price
- **Order**: Id, UserId, Date, TotalAmount

### Relationships
- A User can have many Quotations and Orders.
- A Quotation can have many QuotationItems.
- Each QuotationItem is linked to one Item.

## Diagram
![ERD Diagram](images/erd.png)

