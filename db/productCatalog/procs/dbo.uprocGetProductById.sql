USE [ProductCatalog]
go
IF OBJECT_ID('[dbo].[uprocGetProductById]',N'P') IS NOT NULL
BEGIN
	DROP PROC [dbo].[uprocGetProductById];
	PRINT 'Dropped proc [dbo].[uprocGetProductById]';
END

GO

CREATE PROC [dbo].[uprocGetProductById]
(
@ProductId	INT
)
AS
/*
Usage: Exec [dbo].[uprocGetProductById] @ProductId = 3;
*/

SELECT	a.ProductId,
			b.ProductCategoryId,
			b.CategoryDescr,
			a.IsShippable,
			a.IsVisible,
			a.Price,
			a.ProductName,
			a.ProductDescr
	FROM [dbo].[Product] a INNER JOIN [dbo].[ProductCategory] b ON a.ProductCategoryId = b.ProductCategoryId
	WHERE a.ProductId = @ProductId;

GO