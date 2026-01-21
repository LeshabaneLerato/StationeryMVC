# StationeryMVC – Entity Relationship Diagram

## 1. Entities

### StationeryItem
- Id (Primary Key)
- Name
- Category
- Quantity
- Price
- ImageUrl
- QRCodeImagePath

### Quotation
- Id (Primary Key)
- CustomerName
- Date
- TotalAmount

### QuotationItem
- Id (Primary Key)
- QuotationId (Foreign Key)
- StationeryItemId (Foreign Key)
- Quantity
- UnitPrice

### AppSettings
- Id (Primary Key)
- ShopName
- Slogan
- LogoPath

## 2. Relationships

- One Quotation can have many QuotationItems.
- One StationeryItem can appear in many QuotationItems.
- QuotationItem links Quotation and StationeryItem.

## 3. ERD Representation

Quotation 1 ---- * QuotationItem * ---- 1 StationeryItem
