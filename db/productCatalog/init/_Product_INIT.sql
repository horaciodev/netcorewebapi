USE [ProductCatalog]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[Product] a WHERE a.[ProductCategoryId]=1)
BEGIN
	INSERT INTO [dbo].[Product]([ProductCategoryId],[ProductName],[ProductDescr],[Price],[IsVisible],[IsShippable])
	VALUES(1,'Greens','Holistic body massage for sore muscles.',80,1,0),
		  (1,'Citrus','Natural face treatment with seasonal fruits, vegetables and clay from hot springs.',100,1,0),
		  (1,'Roots','Best Pedicure ever that will make you feel rejuvenated',70,1,0),
		  (1,'Orange','Sauna with your favorite organic iced tea surrounded by spices and aroma therapy.',65,1,0),
		  (1,'Coconut','Hot rock treatment while partially submerged in salt water.',120,1,0)

END

GO

IF NOT EXISTS(SELECT * FROM [dbo].[Product] a WHERE a.[ProductCategoryId]=2)
BEGIN
	INSERT INTO [dbo].[Product]([ProductCategoryId],[ProductName],[ProductDescr],[Price],[IsVisible],[IsShippable])
	VALUES(2,'Avocado moisturizing cream','Organic avocado moisturizing skin cream. 6oz',20,1,1),
		  (2,'Aroma therapy candles','5 seasonal aroma therapy candles.',35,1,1);		 
END

GO

