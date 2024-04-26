CREATE DATABASE RestaurentManager
GO

USE RestaurentManager
GO

CREATE TABLE [User](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Avatar] VARCHAR(255),
	[Email] VARCHAR(255) NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[Role] VARCHAR(255) NOT NULL
)
GO

CREATE TABLE [Food](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Image] VARCHAR(255) NOT NULL,
	[Price] FLOAT NOT NULL,
	[Description] NVARCHAR(500),
	[TypeId] INT NOT NULL
)
GO

CREATE TABLE [Table](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Status] BIT DEFAULT 1
)
GO

CREATE TABLE [Bill](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TableId] INT NOT NULL FOREIGN KEY REFERENCES [Table](Id),
	[SubTotal] FLOAT NOT NULL,
	[ServiceFee] FLOAT DEFAULT 0,
	[Tax] FLOAT DEFAULT 0,
	[Total] FLOAT NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[PaiAt] DATETIME NULL
)
GO

CREATE TABLE [BillRecord](
	[BillId] INT NOT NULL FOREIGN KEY REFERENCES [Bill](Id),
	[FoodId] INT NOT NULL,
	[FoodName] NVARCHAR(255) NOT NULL,
	[Quantity] INT NOT NULL,
	[Price] FLOAT NOT NULL,
	[Note] NVARCHAR(500),
	[Status] INT DEFAULT 1, 
	/* 1: Đang chờ (waiting), 2: Đang xử lý (cooking), 3: Đang trả khách (shipping), 4: Đã trả ra bàn (done) */
)
GO

INSERT INTO [User]([Name], [Email], [Password], [Role]) VALUES
(N'admin','admin@gmai.com', 'e10adc3949ba59abbe56e057f20f883e', 'Admin') /* pass: 123456 */