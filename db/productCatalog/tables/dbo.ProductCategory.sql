USE [ProductCatalog]
GO

CREATE TABLE [dbo].[ProductCategory]
(
[ProductCategoryId]			INT NOT NULL,
[CategoryDescr]				NVARCHAR(50) NOT NULL,
CONSTRAINT PK_ProductCategory PRIMARY KEY CLUSTERED ([ProductCategoryId]) ON [PRIMARY]
);
GO

