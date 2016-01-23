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




GO
/****** Object:  StoredProcedure [Product].[GetReturnDetailsByDate]    Script Date: 09/01/2016 13:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetSalesDetailsByOrderId] '56'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-01-06
-- Description:	Get details of returned product by date
-- ================================================

ALTER PROCEDURE [Product].[GetReturnDetailsByDate]
	(
		@Date datetime
	)
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, CONVERT(varchar, Product.ProductReturn.DateReturned, 105) AS ReturnedOn, 
                         Product.ProductReturn.Quantity, Product.ProductReturn.QuantityActual, Product.ProductReturn.QuantityLower, Product.ProductType.StockAs, Product.Measure.Lower, Product.Measure.Actual, 
                         CONVERT(varchar, Product.SalesDetail.DateSold , 105) AS SoldOn
FROM            Product.Product INNER JOIN
                         Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                         Product.ProductReturn ON Product.ProductDetail.ProductDetailId = Product.ProductReturn.ProductDetailId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.SalesDetail ON Product.ProductDetail.ProductDetailId = Product.SalesDetail.ProductDetailId AND Product.ProductReturn.SalesDetailId = Product.SalesDetail.SalesDetailId
WHERE        (CAST(Product.ProductReturn.DateReturned AS Date) = CAST(@Date AS Date))
ORDER BY     Product.ProductReturn.DateReturned


GO
/****** Object:  StoredProcedure [Product].[GetReturnDetailsByDateRange]    Script Date: 09/01/2016 14:18:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetSalesDetailsByOrderId] '56'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-01-09
-- Description:	Get details of returned product by date range
-- ================================================

ALTER PROCEDURE [Product].[GetReturnDetailsByDateRange]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, CONVERT(varchar, Product.ProductReturn.DateReturned, 105) AS ReturnedOn, 
                         Product.ProductReturn.Quantity, Product.ProductReturn.QuantityActual, Product.ProductReturn.QuantityLower, Product.ProductType.StockAs, Product.Measure.Lower, Product.Measure.Actual, 
                         CONVERT(varchar, Product.SalesDetail.DateSold , 105) AS SoldOn
FROM            Product.Product INNER JOIN
                         Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                         Product.ProductReturn ON Product.ProductDetail.ProductDetailId = Product.ProductReturn.ProductDetailId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.SalesDetail ON Product.ProductDetail.ProductDetailId = Product.SalesDetail.ProductDetailId AND Product.ProductReturn.SalesDetailId = Product.SalesDetail.SalesDetailId
WHERE Cast(Product.ProductReturn.DateReturned As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)

ORDER BY     Product.ProductReturn.DateReturned



GO
/****** Object:  StoredProcedure [Product].[GetStockDatails]    Script Date: 10/01/2016 10:14:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of Stock
-- ================================================

ALTER PROCEDURE [Product].[GetStockDatails]

AS
SELECT        Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, Product.ProductDetail.QuantityLower, 
                         Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.UnitPrice, CONVERT(varchar, Product.ProductDetail.DateReceived, 105) AS DateReceived, 
                         CAST(ROUND(dbo.fn_CalculatePrice(Product.Measure.Volume, Product.Product.ContainsQty, Product.ProductDetail.UnitPrice, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower), 0) AS numeric(36, 2)) AS NetPrice, Product.ProductDetail.SellingPrice, CAST(ROUND(dbo.fn_CalculatePrice(Product.Measure.Volume, Product.Product.ContainsQty, Product.ProductDetail.SellingPrice, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower), 0) AS numeric(36, 2)) AS TotalSellingPrice
FROM            Product.Product INNER JOIN
                         Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.ProductDetail.Status = 1)
ORDER BY      Product.Product.ProductId