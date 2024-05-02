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
	[PaidAt] DATETIME NULL
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
	/* 1: Đang chờ (waiting), 2: Đang xử lý (cooking), 3: Đã trả ra bàn (done) */
)
GO
/* DB of User*/
INSERT INTO [User]([Name], [Email], [Password], [Role]) VALUES
(N'Khanh','admin@gmai.com', '41933e60e9c19b866b3d68864727afe7', 'Admin') /* pass: 123456 */
Go
INSERT INTO [User]([Name], [Email], [Password], [Role]) VALUES
(N'Dung','waiter@gmai.com', '508df4cb2f4d8f80519256258cfb975f', 'Waiter') /* pass: 234567 */
Go
INSERT INTO [User]([Name], [Email], [Password], [Role]) VALUES
(N'Chi','chef@gmai.com', '5bd2026f128662763c532f2f4b6f2476', 'Chef') /* pass: 345678 */
Go
/* DB of food*/
/* typeid: 1: salad, 2:beefsteak, 3: dessert, 4: drink*/

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad with vegetables and olives','/images/salad1','100.000','salad with vegetables and olives','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad with vegetables and fruits','/images/salad2','100.000','salad with vegetables and fruits','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad vegetables, fish, eggs and butter sauce','/images/salad3','120.000','salad vegetables, fish, eggs and butter sauce ','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad vegetables,tomatoes and mayonnaise','/images/salad4','160.000','salad vegetables,tomatoes and mayonnaise','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad vegetable sauce with pickles and tomatoes','/images/salad5','130.000','salad vegetable sauce with pickles and tomatoes','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Salad with beef, eggs, avocado and cream sauce','/images/salad6','140.000','salad with beef, eggs, avocado and cream sauce','1')
go

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with green chili sauce (80g) ','/images/Beefsteak1','100.000','beef with green chili','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with green chili sauce (120g) ','/images/Beefsteak1','190.000','beef with green chili','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with mushroom chili sauce (80g) ','/images/Beefsteak2','120.000','beef with mushroom','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with mushroom chili sauce (120g) ','/images/Beefsteak2','210.000','beef with mushroom','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with cheese sauce with pepper (80g) ','/images/Beefsteak3','150.000','beef with cheese sauce with pepper','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with cheese sauce with pepper (120g) ','/images/Beefsteak3','210.000','beef with cheese sauce with pepper','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with soy sauce with coriander(80g)','/images/Beefsteak4','130.000','beef with soy sauce with coriander','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with soy sauce with coriander(120g)','/images/Beefsteak4','220.000','beef with soy sauce with coriander','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with garlic and butter sauce(80g)','/images/Beefsteak5','110.000','beef with garlic and butter sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with garlic and butter sauce(120g)','/images/Beefsteak5','210.000','beef with garlic and butter sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with garlic and tomato sauce(80g)','/images/Beefsteak6','220.000','beef with garlic and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with garlic and tomato sauce(120g)','/images/Beefsteak6','290.000','beef with garlic and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with chilli pepper and rosemary(80g)','/images/Beefsteak7','180.000','beef with chilli pepper and rosemary','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with chilli pepper and rosemary(120g)','/images/Beefsteak7','290.000','beef with chilli pepper and rosemary','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with vegetables and potato sauce(80g)','/images/Beefsteak8','140.000','Beefsteak with vegetables and potato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with vegetables and potato sauce(120g)','/images/Beefsteak8','200.000','Beefsteak with vegetables and potato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with vegetables and tomato sauce(80g)','/images/Beefsteak9','200.000','Beefsteak with vegetables and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with vegetables and tomato sauce(120g)','/images/Beefsteak9','290.000','Beefsteak with vegetables and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Golden beefsteak and tomato sauce(80g)','/images/Beefsteak10','300.000','Golden beefsteak and tomato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Golden beefsteak and tomato sauce(120g)','/images/Beefsteak10','490.000','Golden beefsteak and tomato','2')
go



INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Cheese cake','/images/tm1','100.000','Cheese and little salt','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Panacosta','/images/tm2','100.000','Panacosta with stawberry','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Chocolate and vanilla ice cream','/images/tm3','100.000','Chocolate and vanilla ice cream','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Melted chocolate cake','/images/tm4','100.000','Melted chocolate cake','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Flan cake ','/images/tm5','100.000','cake with flan on top','3')
go

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Coca cola','/images/drink1','20.000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Pepsi','/images/drink2','20.000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Red bull','/images/drink3','20.000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Aquafina','/images/drink4','10.000',NULL,'4')
go

/*db Table*/
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T1', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T2', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T3', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T4', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T5', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T6', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T7', 0)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T8', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T9', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T10', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T11', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T12', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T13', 0)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T14', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T15', 1)
go
INSERT INTO [Table]([Name], [Status]) VALUES
(N'T16', 1)
go


/*db Bill
*/
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(1, 300.000, 20.000, 30.000, 350.000, '2024-05-01 18:10', '2024-05-01 20:00')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(2, 600.000, 20.000, 60.000, 680.000, '2024-05-01 18:30', '2024-05-01 19:30')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(3, 540.000, 20.000, 54.000, 614.000, '2024-05-01 19:05', '2024-05-01 19:58')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(4, 320.000, 20.000, 32.000, 372.000, '2024-05-01 19:10', '2024-05-01 20:10')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(5, 220.000, 20.000, 22.000, 262.000, '2024-05-01 19:10', '2024-05-01 19:58')
go


/*db BillRecord
Status 1: Đang chờ (waiting), 2: Đang xử lý (cooking), 3: Đã trả ra bàn (done) */
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 1, N'Salad with vegetables and olives', NULL, 100.000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 19, N'Beefsteak with chilli pepper and rosemary(80g)', NULL, 180.000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 33, N'Pepsi', NULL, 20.000, 1, 2)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 26, N'Golden beefsteak and tomato sauce(120g)', NULL, 490.000, 1, 2)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 2, N'Salad with vegetables and fruits', NULL, 100.000, 1, 2)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 35, N'Aquafina', NULL, 10.000, 1, 1)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 9, N'Beefsteak with mushroom chili sauce (80g)', NULL, 120.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 7, N'Beefsteak with green chili sauce (80g) ', NULL, 100.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 3, N'Salad vegetables, fish, eggs and butter sauce', 'No eggs', 120.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 27, N'Cheese cake', NULL, 100.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 28, N'Panacosta', NULL, 100.000, 1, 1)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(4, 14, N'Beefsteak with soy sauce with coriander(120g)', NULL, 220.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(4, 30, N'Melted chocolate cake', NULL, 100.000, 1, 1)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(5, 23, N'Beefsteak with vegetables and tomato sauce(80g)', NULL, 200.000, 1, 1)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(5, 32, N'Coca cola', NULL, 20.000, 1, 1)
go