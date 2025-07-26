IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Invoices' AND xtype='U')
BEGIN
    CREATE TABLE Invoices (
        InvoiceId UNIQUEIDENTIFIER PRIMARY KEY,
        InvoiceNo BIGINT IDENTITY(1,1) NOT NULL,
        InvoiceDate DATETIME NOT NULL,
        CustomerName NVARCHAR(255),
        DiscountPercent DECIMAL(5,2),
        TaxPercent DECIMAL(5,2)
    );
END;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='InvoiceItems' AND xtype='U')
BEGIN
    CREATE TABLE InvoiceItems (
        InvoiceItemId UNIQUEIDENTIFIER PRIMARY KEY,
        InvoiceId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Invoices(InvoiceId),
        Description NVARCHAR(255),
        Quantity INT,
        UnitPrice DECIMAL(10,2)
    );
END;