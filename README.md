# LiteBiller

**LiteBiller** is a professional-grade billing system built as a Windows Forms application using the MVP (Model-View-Presenter) pattern. It’s inspired by MYOB AccountRight, designed for simplicity, scalability, and maintainability.

## 🧱 Tech Stack

- C# (.NET Framework 4.8)
- Windows Forms (WinForms)
- SQL Server (Full Edition)
- ADO.NET (No EF, for performance & control)
- MVP Architecture

## ✅ Features (as of initial commit)

- Create & save invoices
- Auto-generated `InvoiceId` (GUID) and `InvoiceNo` (BIGINT)
- `InvoiceNo` is sequential and used as display ID (`INV-000001`)
- Line items with Quantity, Unit Price, and Total
- Real-time subtotal & total calculation
- Validation for customer name, dates, and items
- Data saved to SQL Server
- Clean form reset after save

## 📚 Design Pattern

This app uses **Model-View-Presenter (MVP)**:
- `IInvoiceView` (interface)
- `InvoiceForm` (view)
- `InvoicePresenter` (presenter)
- `InvoiceRepository` (data access)

## 🗃️ Database Schema (Manual Setup)

```sql
CREATE TABLE Invoices (
    InvoiceId UNIQUEIDENTIFIER PRIMARY KEY,
    InvoiceNo BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
    CustomerName NVARCHAR(255) NOT NULL,
    InvoiceDate DATETIME NOT NULL
);

CREATE TABLE InvoiceItems (
    InvoiceItemId UNIQUEIDENTIFIER PRIMARY KEY,
    InvoiceId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Invoices(InvoiceId) ON DELETE CASCADE,
    Description NVARCHAR(255) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL
);

⚠️ Make sure your connection string is defined in LiteBiller.UI\App.config under LiteBillerDb.
