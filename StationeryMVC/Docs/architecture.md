# StationeryMVC – System Architecture

## 1. Architecture Overview
StationeryMVC follows the Model-View-Controller (MVC) architecture pattern.
This architecture separates the application into three main layers:
Model, View, and Controller.

## 2. MVC Architecture

### 2.1 Model Layer
The Model layer represents the application’s data and business rules.
It includes the following models:
- StationeryItem
- Quotation
- QuotationItem
- AppSettings
- ErrorViewModel

### 2.2 View Layer
The View layer is responsible for displaying data to the user.
It uses Razor views with HTML, CSS, and Bootstrap.

### 2.3 Controller Layer
The Controller layer handles user requests and business logic.
It communicates between the Model and View layers.

## 3. Technology Stack
- Frontend: HTML, CSS, Bootstrap, Razor Views
- Backend: ASP.NET Core MVC (C#)
- Database: SQL Server
- ORM: Entity Framework Core
- QR Code Generation: QR Code library
