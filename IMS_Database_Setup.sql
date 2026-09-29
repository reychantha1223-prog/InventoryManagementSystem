-- ========================================================
-- 1. CREATE DATABASE & USE IT
-- ========================================================
CREATE DATABASE IMSDB;
GO

USE IMSDB;
GO

-- ========================================================
-- 2. CREATE TABLES
-- ========================================================

-- Users
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NULL,
    Role NVARCHAR(20) NULL,
    IsActive BIT NULL DEFAULT 1,
    Email NVARCHAR(100) NULL,
    ProfileImage VARBINARY(MAX) NULL
);
GO

-- Categories
CREATE TABLE dbo.Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    Status NVARCHAR(20) NULL DEFAULT 'Active',
    CreatedAt DATETIME NULL DEFAULT GETDATE()
);
GO

-- Suppliers
CREATE TABLE dbo.Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    ContactPerson NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    CreatedAt DATETIME NULL DEFAULT GETDATE()
);
GO

-- Customers
CREATE TABLE dbo.Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    CreatedAt DATETIME NULL DEFAULT GETDATE()
);
GO

-- Products
CREATE TABLE dbo.Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    Stock INT NOT NULL DEFAULT 0,
    CategoryID INT NULL,
    SupplierID INT NULL,
    Status AS (
        CASE 
            WHEN Stock = 0 THEN 'Out of Stock'
            WHEN Stock <= 5 THEN 'Low Stock'
            ELSE 'In Stock'
        END
    ),
    CreatedAt DATETIME NULL DEFAULT GETDATE(),
    Descriptions NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryID) REFERENCES dbo.Categories(CategoryID) ON DELETE SET NULL,
    CONSTRAINT FK_Products_Suppliers FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID) ON DELETE SET NULL
);
GO

-- Orders
CREATE TABLE dbo.Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    OrderNumber AS ('ORD-' + RIGHT('0000' + CAST(OrderID AS VARCHAR(10)), 4)),
    CustomerID INT NULL,
    OrderDate DATETIME NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NULL DEFAULT 0.00,
    Status NVARCHAR(20) NULL DEFAULT 'Pending',
    Description NVARCHAR(255) NULL,
    Discount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID) ON DELETE SET NULL
);
GO

-- OrderDetails
CREATE TABLE dbo.OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NULL,
    ProductID INT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    SubTotal AS (CAST(Quantity * UnitPrice AS DECIMAL(29,2))),
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES dbo.Orders(OrderID) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductID) REFERENCES dbo.Products(ProductID) ON DELETE CASCADE
);
GO

-- Invoices
CREATE TABLE dbo.Invoices (
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceDate DATETIME NOT NULL DEFAULT GETDATE(),
    CustomerID INT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    PaymentMethod NVARCHAR(50) NULL DEFAULT 'Cash',
    Status NVARCHAR(20) NULL DEFAULT 'Paid',
    Notes NVARCHAR(MAX) NULL,
    CreatedBy NVARCHAR(100) NULL DEFAULT 'admin',
    CreatedAt DATETIME NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID) ON DELETE SET NULL
);
GO

-- InvoiceItems
CREATE TABLE dbo.InvoiceItems (
    InvoiceItemID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    CONSTRAINT FK_InvoiceItems_Invoices FOREIGN KEY (InvoiceID) REFERENCES dbo.Invoices(InvoiceID) ON DELETE CASCADE,
    CONSTRAINT FK_InvoiceItems_Products FOREIGN KEY (ProductID) REFERENCES dbo.Products(ProductID) ON DELETE CASCADE
);
GO

-- ========================================================
-- 3. INSERT USERS ONLY
-- ========================================================
INSERT INTO dbo.Users (Username, PasswordHash, FullName, Role, IsActive, Email) 
VALUES ('admin', 'admin123', 'System Administrator', 'Admin', 1, 'admin@example.com');
GO