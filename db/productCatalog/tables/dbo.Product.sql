USE [ProductCatalog]
GO

IF OBJECT_ID('[dbo].[Product]',N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[Product]
	(
	[ProductId]				INT				NOT NULL IDENTITY(1,1),
	[ProductCategoryId]		INT				NOT NULL,
	[ProductName]			NVARCHAR(50)	NOT NULL,
	[ProductDescr]			NVARCHAR(250)	NULL,
	[Price]					DECIMAL(10,2)	NOT NULL,
	[IsVisible]				BIT				NOT NULL,
	[IsShippable]			BIT				NOT NULL,
	CONSTRAINT PK_Product PRIMARY KEY CLUSTERED ([ProductId]) ON [PRIMARY]
	);
END

GO

IF OBJECT_ID('[dbo].[Product]',N'U') IS NOT NULL
BEGIN
ALTER TABLE [dbo].[Product] ADD CONSTRAINT FK_Product_ProductCategory FOREIGN KEY ([ProductCategoryId]) 
REFERENCES [dbo].[ProductCategory]([ProductCategoryId]);

END

GO


IF OBJECT_ID('[dbo].[Product]',N'U') IS NOT NULL
BEGIN
ALTER TABLE [dbo].[Product] ADD CONSTRAINT DF_Product_IsVisible DEFAULT(1) FOR [IsVisible];
END


IF OBJECT_ID('[dbo].[Product]',N'U') IS NOT NULL
BEGIN
ALTER TABLE [dbo].[Product] ADD CONSTRAINT DF_Product_IsShippable DEFAULT(1) FOR [IsShippable];
END

GO




