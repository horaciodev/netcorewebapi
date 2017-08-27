USE [ProductCatalog]
go
IF OBJECT_ID('[dbo].[uprocGetProductsByCategory]',N'P') IS NOT NULL
BEGIN
	DROP PROC [dbo].[uprocGetProductsByCategory];
	PRINT 'Dopped proc [dbo].[uprocGetProductsByCategory]';
END

GO

CREATE PROC [dbo].[uprocGetProductsByCategory]
(
@ProductCategoryId INT
)
AS
/*
Usage: Exec [dbo].[uprocGetProductsByCategory] @ProductCategoryId = 1;
*/

BEGIN

	SELECT	a.ProductId,
			b.ProductCategoryId,
			b.CategoryDescr,
			a.IsShippable,
			a.IsVisible,
			a.Price,
			a.ProductName,
			a.ProductDescr
	FROM [dbo].[Product] a INNER JOIN [dbo].[ProductCategory] b ON a.ProductCategoryId = b.ProductCategoryId
	Where b.ProductCategoryId = @ProductCategoryId;

END

GO