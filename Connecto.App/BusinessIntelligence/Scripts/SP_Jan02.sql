GO
/****** Object:  StoredProcedure [Product].[GetStockDatailsByProductType]    Script Date: 02/01/2016 01:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2016-01-01
-- Description:	Get details of Stock by product type
-- ================================================

ALTER PROCEDURE [Product].[GetStockDatailsByProductType]
	(
		@ProductName varchar(50)
	)
AS
SELECT        Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, Product.ProductDetail.QuantityLower, 
              Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.UnitPrice,convert(varchar,  Product.ProductDetail.DateReceived, 105) As DateReceived,
			  cast(round(dbo.fn_CalculatePrice(Product.Measure.Volume, Product.ContainsQty, Product.ProductDetail.UnitPrice, 
			  Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, Product.ProductDetail.QuantityLower),0) as numeric(36,2)) As NetPrice
FROM          Product.Product INNER JOIN
              Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
              Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
              Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.ProductDetail.Status = 1) AND (Product.ProductType.Type = @ProductName)
ORDER BY      Product.Product.ProductId