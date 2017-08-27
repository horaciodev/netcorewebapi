USE [ProductCatalog]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[ProductCategory])
BEGIN
	INSERT INTO [dbo].[ProductCategory]([ProductCategoryId],[CategoryDescr])
	VALUES(1,'Services'),
		  (2,'Products');
END

GO
