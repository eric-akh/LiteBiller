# LiteBiller

**LiteBiller** is a simplified WinForms-based billing system inspired by tools like MYOB AccountRight. It enables users to create, view, and manage invoices in a desktop environment using C#, ADO.NET, and SQL Server.

---

## 🎯 Key Features

- Create professional invoices with multiple line items
- Auto-incrementing, sequential `InvoiceNo` (e.g., INV-000001)
- Support for discounts and tax percentages
- Real-time subtotal, tax, discount, and total calculations
- Persist invoices and invoice items to SQL Server
- Clean MVP architecture (Model–View–Presenter)
- Responsive and user-friendly WinForms UI

---

## 🧱 Tech Stack

- .NET Framework 4.7.2
- C# (WinForms)
- ADO.NET for database access
- SQL Server (not LocalDB)
- MVP architectural pattern

---

## 💾 Database Schema Overview

### `Invoices` Table
| Column            | Type          | Description                          |
|------------------|---------------|--------------------------------------|
| InvoiceNo         | `BIGINT IDENTITY` | Human-friendly sequential invoice number |
| InvoiceId         | `UNIQUEIDENTIFIER` | Internal GUID-based identifier       |
| InvoiceDate       | `DATETIME`    | Date and time of invoice creation    |
| CustomerName      | `NVARCHAR`    | Name of the customer                 |
| DiscountPercent   | `DECIMAL(5,2)`| Discount applied (e.g., 10.00 = 10%) |
| TaxPercent        | `DECIMAL(5,2)`| Tax applied (e.g., 10.00 = 10%)      |

### `InvoiceItems` Table
| Column            | Type             | Description                     |
|------------------|------------------|---------------------------------|
| InvoiceItemId     | `UNIQUEIDENTIFIER` | Unique ID for the item          |
| InvoiceId         | `UNIQUEIDENTIFIER` | Foreign key to the invoice      |
| Description       | `NVARCHAR`       | Description of the item/service |
| Quantity          | `INT`            | Number of units                 |
| UnitPrice         | `DECIMAL(18,2)`  | Price per unit                  |

---

## 🚀 Getting Started

1. Clone the repo:
	git clone https://github.com/eric-akh/LiteBiller.git

2. Open the solution in Visual Studio.

3. Update your connection string in `App.config`:
```xml
<connectionStrings>
  <add name="LiteBillerDb" connectionString="Data Source=.;Initial Catalog=LiteBillerDb;Integrated Security=True;" />
</connectionStrings>
```

4. Create the database schema using the scripts/CreateTables.sql (or run your own CREATE TABLE statements based on the structure above).

⚠️ Make sure your connection string is defined in LiteBiller.UI\App.config under LiteBillerDb.
