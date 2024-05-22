CREATE DATABASE RestaurentManager
GO

USE RestaurentManager
GO

CREATE TABLE [User](
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Avatar] VARCHAR(255),
	[Phone] VARCHAR(255) NOT NULL,
	[Birthday] DATE,
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
	[Status] BIT DEFAULT 1,
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

CREATE TABLE [Notification](
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL FOREIGN KEY REFERENCES [User](Id),
	[Content] NVARCHAR(255) NOT NULL,
	[RequiredPayment] INT,
	[Url] VARCHAR(255) NOT NULL,
	[IsRead] BIT DEFAULT 0,
	[CreatedAt] DATETIME DEFAULT GETDATE()
)
GO

/* DB of User*/
INSERT INTO [User]([Name], [Email], [Phone], [Birthday], [Password], [Role]) VALUES
(N'Khanh','admin@gmail.com', '0923654789', '2000/02/14', 'e10adc3949ba59abbe56e057f20f883e', 'Admin') /* pass: 123456 */
Go
INSERT INTO [User]([Name], [Email], [Phone], [Birthday], [Password], [Role]) VALUES
(N'Dung','waiter@gmail.com', '0923456789', '2003/10/11', 'e10adc3949ba59abbe56e057f20f883e', 'Waiter') /* pass: 123456 */
Go
INSERT INTO [User]([Name], [Email], [Phone], [Birthday], [Password], [Role]) VALUES
(N'Chi','chef@gmail.com', '0963258741', '2006/08/29', 'e10adc3949ba59abbe56e057f20f883e', 'Chef') /* pass: 123456 */
Go
/* DB of food*/
/* typeid: 1: salad, 2:beefsteak, 3: dessert, 4: drink*/

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Caesar salad','/images/salad1.jpg','100000','Salad with vegetables and olives','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Ciao baby Salad','/images/salad2.jpg','100000','Salad with vegetables and fruits','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Barbabietole','/images/salad3.jpg','120000','Salad vegetables, fish, eggs and butter sauce ','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Pomodoro salad','/images/salad4.jpg','160000','Salad vegetables,tomatoes and mayonnaise','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Antipasti salad','/images/salad5.jpg','130000','Salad vegetable sauce with pickles and tomatoes','1')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Cream salad','/images/salad6.jpg','140000','Salad with beef, eggs, avocado and cream sauce','1')
go

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Green chili beef (80g)','/images/Beefsteak1.jpg','100000','Beef with green chili','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Green chili beef (120g)','/images/Beefsteak1.jpg','190000','Beef with green chili','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Mushroom chili beef (80g)','/images/Beefsteak2.jpg','120000','Beef with mushroom','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Mushroom chili beef (120g)','/images/Beefsteak2.jpg','210000','Beef with mushroom','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Cheese pepper beef (80g) ','/images/Beefsteak3.jpg','150000','Beef with cheese sauce with pepper','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Cheese pepper beef (120g) ','/images/Beefsteak3.jpg','210000','Beef with cheese sauce with pepper','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with soy coriander(80g)','/images/Beefsteak4.jpg','130000','Beef with soy sauce with coriander','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak with soy coriander(120g)','/images/Beefsteak4.jpg','220000','Beef with soy sauce with coriander','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak butter mix garlic(80g)','/images/Beefsteak5.jpg','110000','Beef with garlic and butter sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak butter mix garlic(120g)','/images/Beefsteak5.jpg','210000','Beef with garlic and butter sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak garlic mix tomato(80g)','/images/Beefsteak6.jpg','220000','Beef with garlic and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak garlic mix tomato(120g)','/images/Beefsteak6.jpg','290000','Beef with garlic and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak rosemary(80g)','/images/Beefsteak7.jpg','180000','Beef with chill pepper and rosemary','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak rosemary(120g)','/images/Beefsteak7.jpg','290000','Beef with chill pepper and rosemary','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak mix vegetables (80g)','/images/Beefsteak8','140000','Beefsteak with vegetables and potato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak mix vegetables (120g)','/images/Beefsteak8.jpg','200000','Beefsteak with vegetables and potato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak mix fruit (80g)','/images/Beefsteak9.jpg','200000','Beefsteak with vegetables and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Beefsteak mix fruit(120g)','/images/Beefsteak9.jpg','290000','Beefsteak with vegetables and tomato sauce','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Golden beefsteak(80g)','/images/Beefsteak10.jpg','300000','Golden beefsteak and tomato','2')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Golden beefsteak(120g)','/images/Beefsteak10.jpg','490000','Golden beefsteak and tomato','2')
go



INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Cheese cake','/images/tm1.jpg','100000','Cheese and little salts','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Panacosta','/images/tm2.jpg','100000','Panacosta with stawberry','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Choco and vanilla ice cream','/images/tm3.jpg','100000','Chocolate and vanilla ice cream','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Melted chocolate cake','/images/tm4.jpg','100000','Melted chocolate cake','3')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Flan cake ','/images/tm5.jpg','100000','Cake with flan on top','3')
go

INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Coca cola ','/images/drink1.jpg','20000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Pepsi ','/images/drink2.jpg','20000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Red bull ','/images/drink3.jpg','20000',NULL,'4')
go
INSERT INTO Food([Name], [Image], [Price], [Description], [TypeId] ) VALUES
(N'Aquafina ','/images/drink4.jpg','10000',NULL,'4')
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
(1, 300000, 20000, 30000, 350000, '2024-05-01 18:10', '2024-05-01 20:00')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(2, 600000, 20000, 60000, 680000, '2024-05-01 18:30', '2024-05-01 19:30')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(3, 540000, 20000, 54.000, 614000, '2024-05-01 19:05', '2024-05-01 19:58')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(4, 320000, 20000, 32.000, 372000, '2024-05-01 19:10', '2024-05-01 20:10')
go
INSERT INTO [Bill]([TableId],[SubTotal],[ServiceFee], [Tax], [Total], [CreatedAt], [PaidAt]) VALUES
(5, 220000, 20000, 22.000, 262000, '2024-05-01 19:10', '2024-05-01 19:58')
go


/*db BillRecord
Status 1: Đang chờ (waiting), 2: Đang xử lý (cooking), 3: Đã trả ra bàn (done) */
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 1, N'Salad with vegetables and olives', NULL, 100000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 19, N'Beefsteak with chilli pepper and rosemary(80g)', NULL, 180000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(1, 33, N'Pepsi', NULL, 20000, 1, 3)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 26, N'Golden beefsteak and tomato sauce(120g)', NULL, 490000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 2, N'Salad with vegetables and fruits', NULL, 100000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(2, 35, N'Aquafina', NULL, 10000, 1, 3)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 9, N'Beefsteak with mushroom chili sauce (80g)', NULL, 120000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 7, N'Beefsteak with green chili sauce (80g) ', NULL, 100000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 3, N'Salad vegetables, fish, eggs and butter sauce', 'No eggs', 120000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 27, N'Cheese cake', NULL, 100000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(3, 28, N'Panacosta', NULL, 100000, 1, 3)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(4, 14, N'Beefsteak with soy sauce with coriander(120g)', NULL, 220000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(4, 30, N'Melted chocolate cake', NULL, 100000, 1, 3)
go

INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(5, 23, N'Beefsteak with vegetables and tomato sauce(80g)', NULL, 200000, 1, 3)
go
INSERT INTO [BillRecord]([BillId],[FoodId],[FoodName],[Note],[Price],[Quantity],[Status]) VALUES
(5, 32, N'Coca cola', NULL, 20000, 1, 3)
go