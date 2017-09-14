USE [ProductCatalog]
go
IF OBJECT_ID('[dbo].[uprocGetMultipleProductsById]',N'P') IS NOT NULL
BEGIN
	DROP PROC [dbo].[uprocGetMultipleProductsById];
	PRINT 'Dropped proc [dbo].[uprocGetMultipleProductsById]';
END

GO

CREATE PROC [dbo].[uprocGetMultipleProductsById]
(
@ProductIdsTable	IntTableType READONLY
)
AS
/*
Usage: 
Declare @ProductsTable AS IntTableType;

INSERT INTO @ProductsTable(RowKey)
VALUES(3),
	  (5);


Exec [dbo].[uprocGetMultipleProductsById] @ProductsTable;
GO
*/

SELECT	b.ProductId,
			c.ProductCategoryId,
			c.CategoryDescr,
			b.IsShippable,
			b.IsVisible,
			b.Price,
			b.ProductName,
			b.ProductDescr
	FROM @ProductIdsTable a INNER JOIN [dbo].[Product] b ON b.ProductId = a.RowKey 
							INNER JOIN [dbo].[ProductCategory] c ON b.ProductCategoryId = c.ProductCategoryId;

GO