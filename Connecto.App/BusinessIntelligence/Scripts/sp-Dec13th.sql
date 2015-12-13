USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductDetail]    Script Date: 13/12/2015 15:32:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetProductDetail] '78'
-- ================================================
-- Author:		@-Ahamed Zameer
-- Create date: 2015-05-27
-- Description:	Get details of product by order Id
-- ================================================

ALTER PROCEDURE [Product].[GetProductDetail]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        [dbo].[fn_GetProductDisplayInfo](Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) As ProductInfo, Product.ProductDetail.OrderId, Product.ProductDetail.OpeningQuantity, Product.ProductDetail.OpeningQuantityActual, 
                         Product.ProductDetail.OpeningQuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.DateReceived
FROM            Product.ProductDetail INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE Cast(Product.ProductDetail.DateReceived As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)