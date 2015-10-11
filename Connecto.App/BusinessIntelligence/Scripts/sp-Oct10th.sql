USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetStockDatails]    Script Date: 11/10/2015 12:55:53 ******/
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
              Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.UnitPrice,convert(varchar,  Product.ProductDetail.DateReceived, 105) As DateReceived,
			  cast(round(dbo.fn_CalculatePrice(Product.Measure.Volume, Product.ContainsQty, Product.ProductDetail.UnitPrice, 
			  Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, Product.ProductDetail.QuantityLower),0) as numeric(36,2)) As NetPrice
FROM          Product.Product INNER JOIN
              Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
              Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
              Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.ProductDetail.Status = 1)
ORDER BY      Product.Product.ProductId


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailsByOrderId]    Script Date: 11/10/2015 12:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetSalesDetailsByOrderId] '78'
-- ================================================
-- Author:		@-Ahamed Zameer
-- Create date: 2015-05-17
-- Description:	Get details of product by order Id
-- ================================================

ALTER PROCEDURE [Product].[GetSalesDetailsByOrderId]
	(
		@Date datetime
	)
AS
SELECT        [dbo].[fn_GetProductDisplayInfo](Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) As ProductInfo, Product.SalesDetail.Quantity, Product.ProductType.StockAs, Product.SalesDetail.QuantityActual, Product.Measure.Actual, 
                         Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.ProductDetail.UnitPrice , Product.SalesDetail.SellingPrice, Product.SalesDetail.DiscountBy, Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, 
                         Product.SalesDetail.NetPrice, CONVERT(varchar, Product.SalesDetail.DateSold, 105) AS SoldOn, (HumanResource.Person.FirstName + ' ' +  HumanResource.Person.LastName) As CustomerName,
						 cast(round(dbo.fn_CalculateCostPrice(Product.Measure.Volume, Product.ContainsQty, Product.ProductDetail.UnitPrice, 
			  Product.SalesDetail.Quantity, Product.SalesDetail.QuantityActual, Product.SalesDetail.QuantityLower),0) as numeric(36,2)) As CostPrice
FROM            HumanResource.Person INNER JOIN
                         Product.Customer ON HumanResource.Person.PersonId = Product.Customer.PersonId RIGHT OUTER JOIN
                         Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId ON Product.Customer.CustomerId = Product.SalesDetail.CustomerId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date)



USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailForMonth]    Script Date: 11/10/2015 13:00:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetProductDetail] '78'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-28
-- Description:	Get details of Sales for month by Date of sold
-- ================================================

ALTER PROCEDURE [Product].[GetSalesDetailForMonth]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, Product.SalesDetail.Quantity, Product.ProductType.StockAs, 
                         Product.SalesDetail.QuantityActual, Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.SalesDetail.UnitPrice, Product.SalesDetail.DiscountBy, 
                         Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice, CONVERT(varchar, Product.SalesDetail.DateSold, 105) AS SoldOn, Product.SalesDetail.SellingPrice, 
                         HumanResource.Person.FirstName + ' ' + HumanResource.Person.LastName AS CustomerName, CAST(ROUND(dbo.fn_CalculateCostPrice(Product.Measure.Volume, Product.Product.ContainsQty, 
                         Product.ProductDetail.UnitPrice, Product.SalesDetail.Quantity, Product.SalesDetail.QuantityActual, Product.SalesDetail.QuantityLower), 0) AS numeric(36, 2)) AS CostPrice
FROM            HumanResource.Person INNER JOIN
                         Product.Customer ON HumanResource.Person.PersonId = Product.Customer.PersonId RIGHT OUTER JOIN
                         Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId ON Product.Customer.CustomerId = Product.SalesDetail.CustomerId

WHERE Cast(Product.SalesDetail.DateSold As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)

ORDER BY Product.SalesDetail.DateSold


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetPurchaseOrderDatails]    Script Date: 11/10/2015 13:00:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-29
-- Description:	Get details of Purchase Order
-- ================================================

ALTER PROCEDURE [Product].[GetPurchaseOrderDatails]

AS
SELECT        [dbo].[fn_GetProductDisplayInfo](Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) As ProductInfo, Product.Product.StockInHand, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.Product.Reorderlevel
FROM            Product.ProductType INNER JOIN
                         Product.Product ON Product.ProductType.ProductTypeId = Product.Product.ProductTypeId INNER JOIN
                         Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.Product.StockInHand <= Product.Product.Reorderlevel) AND (Product.Product.Status = 1) 
ORDER BY     Product.Product.ProductId



USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductDetail]    Script Date: 11/10/2015 13:00:51 ******/
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
SELECT        [dbo].[fn_GetProductDisplayInfo](Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) As ProductInfo, Product.ProductDetail.OrderId, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.DateReceived
FROM            Product.ProductDetail INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE Cast(Product.ProductDetail.DateReceived As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)



