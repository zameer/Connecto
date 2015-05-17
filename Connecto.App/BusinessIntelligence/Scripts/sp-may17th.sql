USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductDetail]    Script Date: 5/17/2015 5:48:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetProductDetail] '78'
-- ================================================
-- Author:		@-Ahamed Zameer
-- Create date: 2015-05-17
-- Description:	Get details of product by order Id
-- ================================================

CREATE PROCEDURE [Product].[GetProductDetail]
	(
		@Id varchar(8000)
	)
AS
SELECT        Product.ProductDetail.OrderId, Product.Product.Name, Product.ProductDetail.ProductCode, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower
FROM            Product.ProductDetail INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailsByOrderId]    Script Date: 5/17/2015 5:48:52 PM ******/
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

CREATE PROCEDURE [Product].[GetSalesDetailsByOrderId]
	(
		@Id varchar(8000)
	)
AS
SELECT        Product.Product.Name, Product.ProductDetail.ProductCode, Product.SalesDetail.Quantity, Product.ProductType.StockAs, Product.SalesDetail.QuantityActual, 
                         Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.SalesDetail.Price, Product.SalesDetail.DiscountBy, 
                         Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice
FROM            Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.SalesDetail.OrderId = @Id)
GO
