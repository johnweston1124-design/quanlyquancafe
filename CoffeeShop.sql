/* =========================================================
   1. Re-create database (Reset cơ sở dữ liệu)
   ========================================================= */
IF DB_ID(N'CoffeeShopDB') IS NOT NULL
BEGIN
    ALTER DATABASE CoffeeShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CoffeeShopDB;
END;
GO

CREATE DATABASE CoffeeShopDB;
GO

USE CoffeeShopDB;
GO

/* =========================================================
   2. Core schema (ấu trúc dữ liệu cốt lõi)
   ========================================================= */

/* ---------- Employees ---------- */
CREATE TABLE Employees
(
		EmployeeId INT IDENTITY(1,1) NOT NULL,
		FullName NVARCHAR(100) NOT NULL,
		Gender NVARCHAR(10) NULL,
		DateOfBirth DATE NULL,
		Phone NVARCHAR(20) NULL,
		Address NVARCHAR(255) NULL,
		HireDate DATE NOT NULL CONSTRAINT DF_Employees_HireDate DEFAULT (CONVERT(DATE, GETDATE())),
		Salary DECIMAL(18,2) NULL,
		CONSTRAINT PK_Employees PRIMARY KEY (EmployeeId),
		CONSTRAINT CK_Employees_Gender CHECK (Gender IS NULL OR Gender IN (N'Male', N'Female', N'Other')),
		CONSTRAINT CK_Employees_Salary CHECK (Salary IS NULL OR Salary >= 0)
);
GO

/* ---------- Users ---------- */
CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT (1),
    EmployeeId INT NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (UserId),
    CONSTRAINT UQ_Users_Username UNIQUE (Username),
    CONSTRAINT UQ_Users_EmployeeId UNIQUE (EmployeeId),
    CONSTRAINT FK_Users_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT CK_Users_Role CHECK (Role IN (N'Admin', N'Staff'))
);
GO

/* ---------- Categories ---------- */
CREATE TABLE Categories
(
    CategoryId INT IDENTITY(1,1) NOT NULL,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Categories_IsActive DEFAULT (1),
    CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Categories_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2(0) NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryId),
    CONSTRAINT UQ_Categories_CategoryName UNIQUE (CategoryName)
);
GO

/* ---------- Products ---------- */
CREATE TABLE Products
(
    ProductId INT IDENTITY(1,1) NOT NULL,
    ProductName NVARCHAR(150) NOT NULL,
    CategoryId INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(500) NULL,
    Unit NVARCHAR(50) NOT NULL,
    Image NVARCHAR(255) NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Products_Status DEFAULT (N'Available'),
    CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Products_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2(0) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Products_IsActive DEFAULT (1),
    CONSTRAINT PK_Products PRIMARY KEY (ProductId),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT UQ_Products_ProductName_CategoryId UNIQUE (ProductName, CategoryId),
    CONSTRAINT CK_Products_Price CHECK (Price >= 0),
    CONSTRAINT CK_Products_Status CHECK (Status IN (N'Available', N'Unavailable'))
);
GO

/* ---------- CafeTables ---------- */
CREATE TABLE CafeTables
(
    TableId INT IDENTITY(1,1) NOT NULL,
    TableName NVARCHAR(20) NOT NULL,
    Capacity INT NOT NULL,
    Status NVARCHAR(20) NOT NULL CONSTRAINT DF_CafeTables_Status DEFAULT (N'Empty'),
    IsActive BIT NOT NULL CONSTRAINT DF_CafeTables_IsActive DEFAULT (1),
    Zone NVARCHAR(50) NULL,
    CONSTRAINT PK_CafeTables PRIMARY KEY (TableId),
    CONSTRAINT UQ_CafeTables_TableName UNIQUE (TableName),
    CONSTRAINT CK_CafeTables_Capacity CHECK (Capacity > 0),
    CONSTRAINT CK_CafeTables_Status CHECK (Status IN (N'Empty', N'Occupied', N'Reserved'))
);
GO

/* ---------- Orders ---------- */
CREATE TABLE Orders
(
    OrderId INT IDENTITY(1,1) NOT NULL,
    TableId INT NOT NULL,
    EmployeeId INT NOT NULL,
    OrderStatus NVARCHAR(20) NOT NULL CONSTRAINT DF_Orders_OrderStatus DEFAULT (N'Pending'),
    CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Orders_CreatedAt DEFAULT (SYSDATETIME()),
    PaidAt DATETIME2(0) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_Orders_TotalAmount DEFAULT (0),
    DiscountAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_Orders_DiscountAmount DEFAULT (0),
    VATAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_Orders_VATAmount DEFAULT (0),
    SurchargeAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_Orders_SurchargeAmount DEFAULT (0),
    FinalAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_Orders_FinalAmount DEFAULT (0),
    Notes NVARCHAR(500) NULL,
    CONSTRAINT PK_Orders PRIMARY KEY (OrderId),
    CONSTRAINT FK_Orders_CafeTables FOREIGN KEY (TableId) REFERENCES CafeTables(TableId),
    CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT CK_Orders_Status CHECK (OrderStatus IN (N'Pending', N'Paid', N'Cancelled')),
    CONSTRAINT CK_Orders_TotalAmount CHECK (TotalAmount >= 0),
    CONSTRAINT CK_Orders_DiscountAmount CHECK (DiscountAmount >= 0),
    CONSTRAINT CK_Orders_VATAmount CHECK (VATAmount >= 0),
    CONSTRAINT CK_Orders_SurchargeAmount CHECK (SurchargeAmount >= 0),
    CONSTRAINT CK_Orders_FinalAmount CHECK (FinalAmount >= 0),
    CONSTRAINT CK_Orders_FinalFormula CHECK (FinalAmount = TotalAmount - DiscountAmount + VATAmount + SurchargeAmount),
    CONSTRAINT CK_Orders_PaidAt_Status CHECK
    (
        (OrderStatus = N'Paid' AND PaidAt IS NOT NULL AND PaidAt >= CreatedAt)
        OR (OrderStatus IN (N'Pending', N'Cancelled') AND PaidAt IS NULL)
    )
);
GO

/* ---------- OrderDetails ---------- */
CREATE TABLE OrderDetails
(
    OrderDetailId INT IDENTITY(1,1) NOT NULL,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    Notes NVARCHAR(255) NULL,
    CONSTRAINT PK_OrderDetails PRIMARY KEY (OrderDetailId),
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT CK_OrderDetails_Quantity CHECK (Quantity > 0),
    CONSTRAINT CK_OrderDetails_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT CK_OrderDetails_LineTotal CHECK (LineTotal = Quantity * UnitPrice)
);
GO

/* =========================================================
   3. Core indexes
   ========================================================= */

CREATE INDEX IX_Products_CategoryId_IsActive_Status
ON Products(CategoryId, IsActive, Status);
GO

CREATE INDEX IX_CafeTables_Status_IsActive
ON CafeTables(Status, IsActive);
GO

CREATE INDEX IX_Orders_EmployeeId_CreatedAt
ON Orders(EmployeeId, CreatedAt);
GO

CREATE INDEX IX_Orders_OrderStatus_CreatedAt
ON Orders(OrderStatus, CreatedAt);
GO

CREATE UNIQUE INDEX UX_Orders_TableId_Pending
ON Orders(TableId)
WHERE OrderStatus = N'Pending';
GO

CREATE INDEX IX_OrderDetails_OrderId
ON OrderDetails(OrderId);
GO

CREATE INDEX IX_OrderDetails_ProductId
ON OrderDetails(ProductId);
GO

/* =========================================================
   4. Seed data core
   ========================================================= */

/* ---------- Employees ---------- */
INSERT INTO Employees (FullName, Gender, DateOfBirth, Phone, Address, HireDate, Salary)
VALUES
    (N'Nguyễn Văn An', N'Male', '1990-05-10', N'0901000001', N'Quận 1, TP. Hồ Chí Minh', '2022-01-15', 15000000),
    (N'Trần Thị Bình', N'Female', '1995-08-21', N'0901000002', N'Quận 3, TP. Hồ Chí Minh', '2023-03-01', 9000000),
    (N'Lê Hoàng Nam', N'Male', '1998-11-02', N'0901000003', N'Bình Thạnh, TP. Hồ Chí Minh', '2023-06-12', 8500000),
    (N'Phạm Thu Trang', N'Female', '2000-01-18', N'0901000004', N'Gò Vấp, TP. Hồ Chí Minh', '2024-02-20', 8000000),
    (N'Đỗ Minh Khoa', N'Male', '1997-07-07', N'0901000005', N'Thủ Đức, TP. Hồ Chí Minh', '2024-05-10', NULL);
GO

/* ---------- Users ---------- */
INSERT INTO Users (Username, [Password], Role, IsActive, EmployeeId)
VALUES
    (N'admin', N'222222', N'Admin', 1, 1),
    (N'staff', N'222222', N'Staff', 1, 2),
    (N'lockedstaff', N'222222', N'Staff', 0, 4);
GO

/* ---------- Categories ---------- */
INSERT INTO Categories (CategoryName, Description, IsActive, CreatedAt)
VALUES
    (N'Cà phê', N'Các món cà phê nóng và lạnh', 1, '2026-03-01 08:00:00'),
    (N'Trà sữa', N'Các món trà sữa và biến thể', 1, '2026-03-01 08:00:00'),
    (N'Trà trái cây', N'Các món trà trái cây giải khát', 1, '2026-03-01 08:00:00'),
    (N'Đá xay', N'Các món blended, đá xay', 1, '2026-03-01 08:00:00'),
    (N'Bánh ngọt', N'Bánh ăn kèm', 1, '2026-03-01 08:00:00');
GO

/* ---------- Products ---------- */
INSERT INTO Products
(
    ProductName,
    CategoryId,
    Price,
    Description,
    Unit,
    Image,
    Status,
    CreatedAt,
    UpdatedAt,
    IsActive
)
VALUES
    (N'Espresso', 1, 35000, N'Cà phê espresso đậm vị', N'Ly', N'images\\espresso.jpg', N'Available', '2026-03-01 08:10:00', NULL, 1),
    (N'Cà phê đen đá', 1, 29000, N'Cà phê đen truyền thống', N'Ly', N'images\\ca-phe-den-da.jpg', N'Available', '2026-03-01 08:15:00', NULL, 1),
    (N'Cà phê sữa đá', 1, 32000, N'Cà phê sữa đá Việt Nam', N'Ly', N'images\\ca-phe-sua-da.jpg', N'Available', '2026-03-01 08:20:00', NULL, 1),
    (N'Bạc xỉu', 1, 38000, N'Bạc xỉu ngọt nhẹ', N'Ly', N'images\\bac-xiu.jpg', N'Available', '2026-03-01 08:25:00', NULL, 1),
    (N'Trà sữa trân châu', 2, 45000, N'Trà sữa truyền thống kèm trân châu', N'Ly', N'images\\tra-sua-tran-chau.jpg', N'Available', '2026-03-01 08:30:00', NULL, 1),
    (N'Trà sữa matcha', 2, 49000, N'Trà sữa matcha thơm béo', N'Ly', N'images\\tra-sua-matcha.jpg', N'Available', '2026-03-01 08:35:00', NULL, 1),
    (N'Trà đào cam sả', 3, 48000, N'Trà đào với cam và sả', N'Ly', N'images\\tra-dao-cam-sa.jpg', N'Available', '2026-03-01 08:40:00', NULL, 1),
    (N'Trà vải hạt chia', 3, 47000, N'Trà vải thanh mát', N'Ly', N'images\\tra-vai-hat-chia.jpg', N'Available', '2026-03-01 08:45:00', NULL, 1),
    (N'Cookies đá xay', 4, 55000, N'Đá xay cookies kem béo', N'Ly', N'images\\cookies-da-xay.jpg', N'Available', '2026-03-01 08:50:00', NULL, 1),
    (N'Mocha đá xay', 4, 58000, N'Mocha đá xay đậm vị cacao', N'Ly', N'images\\mocha-da-xay.jpg', N'Available', '2026-03-01 08:55:00', NULL, 1),
    (N'Bánh tiramisu', 5, 42000, N'Bánh tiramisu mềm mịn', N'Phần', N'images\\banh-tiramisu.jpg', N'Available', '2026-03-01 09:00:00', NULL, 1),
    (N'Croissant bơ', 5, 30000, N'Bánh sừng bò bơ', N'Phần', N'images\\croissant-bo.jpg', N'Available', '2026-03-01 09:05:00', NULL, 1),
    (N'Bánh phô mai chanh dây', 5, 45000, N'Bánh phô mai vị chanh dây', N'Phần', N'images\\banh-pho-mai-chanh-day.jpg', N'Unavailable', '2026-03-01 09:10:00', NULL, 1),
    (N'Americano', 1, 36000, N'Cà phê americano', N'Ly', N'images\\americano.jpg', N'Available', '2026-03-01 09:15:00', NULL, 1),
    (N'Hồng trà sữa', 2, 44000, N'Hồng trà sữa truyền thống', N'Ly', N'images\\hong-tra-sua.jpg', N'Available', '2026-03-01 09:20:00', NULL, 0);
GO

/* ---------- CafeTables ---------- */
INSERT INTO CafeTables (TableName, Capacity, Status, IsActive, Zone)
VALUES
    (N'T01', 2, N'Empty', 1, N'Tầng 1'),
    (N'T02', 4, N'Occupied', 1, N'Tầng 1'),
    (N'T03', 4, N'Reserved', 1, N'Tầng 1'),
    (N'T04', 6, N'Empty', 1, N'Tầng 2'),
    (N'T05', 2, N'Occupied', 1, N'Tầng 2'),
    (N'T06', 8, N'Empty', 1, N'Khu trong nhà'),
    (N'VIP01', 10, N'Reserved', 1, N'VIP'),
    (N'VIP02', 10, N'Empty', 1, N'VIP');
GO

/* ---------- Orders ---------- */
INSERT INTO Orders
(
    TableId,
    EmployeeId,
    OrderStatus,
    CreatedAt,
    PaidAt,
    TotalAmount,
    DiscountAmount,
    VATAmount,
    SurchargeAmount,
    FinalAmount,
    Notes
)
VALUES
    (1, 2, N'Paid',      '2026-03-20 09:00:00', '2026-03-20 09:35:00',  67000,     0,  6700,    0,  73700, N'Hóa đơn buổi sáng'),
    (4, 3, N'Paid',      '2026-03-20 14:10:00', '2026-03-20 15:00:00', 145000, 10000, 13500,    0, 148500, N'Khách đi nhóm 3 người'),
    (6, 2, N'Cancelled', '2026-03-20 16:20:00', NULL,                      45000,     0,     0,    0,  45000, N'Khách đổi ý trước khi pha'),
    (2, 4, N'Pending',   '2026-03-21 08:15:00', NULL,                      87000,     0,     0,    0,  87000, N'Khách đang dùng tại quán'),
    (5, 2, N'Pending',   '2026-03-21 09:10:00', NULL,                     103000,     0,     0, 5000, 108000, N'Thêm phụ thu đổi size'),
    (1, 3, N'Paid',      '2026-03-21 10:00:00', '2026-03-21 10:30:00', 117000,  5000, 11200,    0, 123200, N'Khách thanh toán thẻ');
GO

/* ---------- OrderDetails ---------- */
INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal, Notes)
VALUES
    (1, 3, 1, 32000, 32000, N'Ít đá'),
    (1, 1, 1, 35000, 35000, NULL),

    (2, 10, 1, 58000, 58000, NULL),
    (2, 5, 1, 45000, 45000, N'Ít đường'),
    (2, 11, 1, 42000, 42000, NULL),

    (3, 5, 1, 45000, 45000, N'Đã hủy trước khi phục vụ'),

    (4, 5, 1, 45000, 45000, N'Ít đá'),
    (4, 11, 1, 42000, 42000, NULL),

    (5, 9, 1, 55000, 55000, NULL),
    (5, 7, 1, 48000, 48000, N'Tăng size M lên L'),

    (6, 3, 1, 32000, 32000, NULL),
    (6, 9, 1, 55000, 55000, NULL),
    (6, 12, 1, 30000, 30000, NULL);
GO

/* =========================================================
   5. Optional extension schema ( lược đồ mở rộng )
   ========================================================= */

/* ---------- Ingredients ---------- */
CREATE TABLE Ingredients
(
    IngredientId INT IDENTITY(1,1) NOT NULL,
    IngredientName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Ingredients_IsActive DEFAULT (1),
    CreatedAt DATETIME2(0) NOT NULL CONSTRAINT DF_Ingredients_CreatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_Ingredients PRIMARY KEY (IngredientId),
    CONSTRAINT UQ_Ingredients_IngredientName UNIQUE (IngredientName)
);
GO

/* ---------- Stock ---------- */
CREATE TABLE Stock
(
    StockId INT IDENTITY(1,1) NOT NULL,
    IngredientId INT NOT NULL,
    QuantityInStock DECIMAL(18,2) NOT NULL,
    MinQuantity DECIMAL(18,2) NOT NULL CONSTRAINT DF_Stock_MinQuantity DEFAULT (0),
    LastUpdated DATETIME2(0) NOT NULL CONSTRAINT DF_Stock_LastUpdated DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_Stock PRIMARY KEY (StockId),
    CONSTRAINT UQ_Stock_IngredientId UNIQUE (IngredientId),
    CONSTRAINT FK_Stock_Ingredients FOREIGN KEY (IngredientId) REFERENCES Ingredients(IngredientId),
    CONSTRAINT CK_Stock_QuantityInStock CHECK (QuantityInStock >= 0),
    CONSTRAINT CK_Stock_MinQuantity CHECK (MinQuantity >= 0)
);
GO

/* ---------- Recipe ---------- */
CREATE TABLE Recipe
(
    RecipeId INT IDENTITY(1,1) NOT NULL,
    ProductId INT NOT NULL,
    IngredientId INT NOT NULL,
    Quantity DECIMAL(18,2) NOT NULL,
    Notes NVARCHAR(255) NULL,
    CONSTRAINT PK_Recipe PRIMARY KEY (RecipeId),
    CONSTRAINT FK_Recipe_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT FK_Recipe_Ingredients FOREIGN KEY (IngredientId) REFERENCES Ingredients(IngredientId),
    CONSTRAINT UQ_Recipe_ProductId_IngredientId UNIQUE (ProductId, IngredientId),
    CONSTRAINT CK_Recipe_Quantity CHECK (Quantity > 0)
);
GO

/* ---------- ProductPriceHistory ---------- */
CREATE TABLE ProductPriceHistory
(
    ProductPriceHistoryId INT IDENTITY(1,1) NOT NULL,
    ProductId INT NOT NULL,
    OldPrice DECIMAL(18,2) NOT NULL,
    NewPrice DECIMAL(18,2) NOT NULL,
    ChangedAt DATETIME2(0) NOT NULL CONSTRAINT DF_ProductPriceHistory_ChangedAt DEFAULT (SYSDATETIME()),
    EmployeeId INT NULL,
    Notes NVARCHAR(255) NULL,
    CONSTRAINT PK_ProductPriceHistory PRIMARY KEY (ProductPriceHistoryId),
    CONSTRAINT FK_ProductPriceHistory_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT FK_ProductPriceHistory_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT CK_ProductPriceHistory_OldPrice CHECK (OldPrice >= 0),
    CONSTRAINT CK_ProductPriceHistory_NewPrice CHECK (NewPrice >= 0),
    CONSTRAINT CK_ProductPriceHistory_ChangedPrice CHECK (OldPrice <> NewPrice)
);
GO

/* ---------- Extension indexes ---------- */
CREATE INDEX IX_Stock_IngredientId
ON Stock(IngredientId);
GO

CREATE INDEX IX_Recipe_ProductId
ON Recipe(ProductId);
GO

CREATE INDEX IX_Recipe_IngredientId
ON Recipe(IngredientId);
GO

CREATE INDEX IX_ProductPriceHistory_ProductId_ChangedAt
ON ProductPriceHistory(ProductId, ChangedAt);
GO

/* =========================================================
   6. Seed data extension
   ========================================================= */

/* ---------- Ingredients ---------- */
INSERT INTO Ingredients (IngredientName, Unit, Description, IsActive, CreatedAt)
VALUES
    (N'Cà phê hạt', N'Gram', N'Nguyên liệu cho các món cà phê', 1, '2026-03-01 07:00:00'),
    (N'Sữa đặc', N'Ml', N'Sữa đặc có đường', 1, '2026-03-01 07:00:00'),
    (N'Sữa tươi', N'Ml', N'Sữa tươi không đường', 1, '2026-03-01 07:00:00'),
    (N'Trà đen', N'Gram', N'Lá trà đen', 1, '2026-03-01 07:00:00'),
    (N'Syrup đào', N'Ml', N'Syrup đào dùng cho trà trái cây', 1, '2026-03-01 07:00:00'),
    (N'Cam lát', N'Slice', N'Cam tươi cắt lát', 1, '2026-03-01 07:00:00'),
    (N'Trân châu', N'Gram', N'Topping trân châu đen', 1, '2026-03-01 07:00:00'),
    (N'Bột matcha', N'Gram', N'Bột matcha nguyên chất', 1, '2026-03-01 07:00:00');
GO

/* ---------- Stock ---------- */
INSERT INTO Stock (IngredientId, QuantityInStock, MinQuantity, LastUpdated)
VALUES
    (1, 5000, 1000, '2026-03-21 07:30:00'),
    (2, 3000,  500, '2026-03-21 07:30:00'),
    (3, 4000,  800, '2026-03-21 07:30:00'),
    (4, 2500,  500, '2026-03-21 07:30:00'),
    (5, 2000,  300, '2026-03-21 07:30:00'),
    (6,  150,   30, '2026-03-21 07:30:00'),
    (7, 3500,  600, '2026-03-21 07:30:00'),
    (8, 1200,  200, '2026-03-21 07:30:00');
GO

/* ---------- Recipe ---------- */
INSERT INTO Recipe (ProductId, IngredientId, Quantity, Notes)
VALUES
    (3, 1, 18, N'Cà phê sữa đá'),
    (3, 2, 30, N'Cà phê sữa đá'),
    (3, 3, 20, N'Cà phê sữa đá'),

    (4, 1, 10, N'Bạc xỉu'),
    (4, 2, 20, N'Bạc xỉu'),
    (4, 3, 80, N'Bạc xỉu'),

    (5, 4, 15, N'Trà sữa trân châu'),
    (5, 3, 120, N'Trà sữa trân châu'),
    (5, 7, 50, N'Trà sữa trân châu'),

    (6, 8, 10, N'Trà sữa matcha'),
    (6, 3, 120, N'Trà sữa matcha'),
    (6, 7, 40, N'Trà sữa matcha'),

    (7, 4, 10, N'Trà đào cam sả'),
    (7, 5, 25, N'Trà đào cam sả'),
    (7, 6,  2, N'Trà đào cam sả');
GO

/* ---------- ProductPriceHistory ---------- */
INSERT INTO ProductPriceHistory (ProductId, OldPrice, NewPrice, ChangedAt, EmployeeId, Notes)
VALUES
    (5, 42000, 45000, '2026-03-05 09:00:00', 1, N'Điều chỉnh giá đầu tháng'),
    (10, 55000, 58000, '2026-03-10 09:00:00', 1, N'Điều chỉnh theo chi phí nguyên liệu'),
    (11, 40000, 42000, '2026-03-12 09:00:00', 1, N'Cập nhật menu mới');
GO

-- Ngăn insert trùng cùng món trong cùng order
ALTER TABLE OrderDetails
ADD CONSTRAINT UQ_OrderDetails_OrderProduct 
UNIQUE (OrderId, ProductId)


