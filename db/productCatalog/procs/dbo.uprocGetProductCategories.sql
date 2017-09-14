USE [ProductCatalog]
go
IF OBJECT_ID('[dbo].[uprocGetProductCategories]',N'P') IS NOT NULL
BEGIN
	DROP PROC [dbo].[uprocGetProductCategories];
	PRINT 'Dropped Proc [dbo].[uprocGetProductCategories]';
END

GO

CREATE PROC [dbo].[uprocGetProductCategories]
AS
/*
Usage: Exec [dbo].[uprocGetProductCategories];
*/
SELECT	a.ProductCategoryId,
		A.CategoryDescr
FROM [dbo].[ProductCategory] a
ORDER BY CategoryDescr

GO