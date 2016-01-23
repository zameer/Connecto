
GO
/****** Object:  StoredProcedure [HumanResource].[GetAllEmployeeDatails]    Script Date: 23/01/2016 10:10:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of All Employee
-- ================================================

ALTER PROCEDURE [HumanResource].[GetAllEmployeeDatails]

AS
SELECT       HumanResource.Employee.EmployeeId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         HumanResource.Person INNER JOIN
             HumanResource.Employee ON HumanResource.Person.PersonId = HumanResource.Employee.PersonId
WHERE        (HumanResource.Employee.Status = 1) 
ORDER BY     HumanResource.Employee.EmployeeId

GO
/****** Object:  StoredProcedure [HumanResource].[GetEmployeeDatails]    Script Date: 23/01/2016 10:10:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-24
-- Description:	Get details of an Employee
-- ================================================

ALTER PROCEDURE [HumanResource].[GetEmployeeDatails]
(
		@Id varchar(50)
)	
AS
SELECT       HumanResource.Employee.EmployeeId, HumanResource.Person.FirstName, HumanResource.Person.LastName, HumanResource.Contact.LandNumber, HumanResource.Contact.MobileNumber, 
             HumanResource.Contact.AddressNo, HumanResource.Contact.AddressStreet, HumanResource.Contact.City, HumanResource.Contact.Province
FROM         HumanResource.Employee INNER JOIN
             HumanResource.Person ON HumanResource.Employee.PersonId = HumanResource.Person.PersonId INNER JOIN
             HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND 
             HumanResource.Person.PersonId = HumanResource.Contact.PersonId
WHERE        (HumanResource.Employee.Status = 1) and (HumanResource.Employee.EmployeeId = @Id)
ORDER BY     HumanResource.Employee.EmployeeId


GO
/****** Object:  StoredProcedure [Product].[GetAllCustomerDatails]    Script Date: 23/01/2016 10:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of All customers
-- ================================================

ALTER PROCEDURE [Product].[GetAllCustomerDatails]

AS
SELECT       Product.Customer.CustomerId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         Product.Customer INNER JOIN
             HumanResource.Person ON Product.Customer.PersonId = HumanResource.Person.PersonId
WHERE        (Product.Customer.Status = 1) 
ORDER BY     Product.Customer.CustomerId


GO
/****** Object:  StoredProcedure [Product].[GetAllSupplierDatails]    Script Date: 23/01/2016 10:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of All Supplier
-- ================================================

ALTER PROCEDURE [Product].[GetAllSupplierDatails]

AS
SELECT        Product.Supplier.SupplierId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM            HumanResource.Person INNER JOIN
                         HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId INNER JOIN
                         Product.Supplier ON HumanResource.Person.PersonId = Product.Supplier.PersonId AND HumanResource.Person.PersonId = Product.Supplier.PersonId
WHERE        (Product.Supplier.Status = 1)
ORDER BY     Product.Supplier.SupplierId

GO
/****** Object:  StoredProcedure [Product].[GetCustomerDatails]    Script Date: 23/01/2016 10:11:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of a customer
-- ================================================

ALTER PROCEDURE [Product].[GetCustomerDatails]
(
		@Id varchar(50)
)
AS
SELECT       Product.Customer.CustomerId, HumanResource.Person.FirstName, HumanResource.Person.LastName, HumanResource.Contact.LandNumber, HumanResource.Contact.MobileNumber, 
             HumanResource.Contact.AddressNo, HumanResource.Contact.AddressStreet, HumanResource.Contact.City, HumanResource.Contact.Province
FROM         Product.Customer INNER JOIN
             HumanResource.Person ON Product.Customer.PersonId = HumanResource.Person.PersonId INNER JOIN
             HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND 
             HumanResource.Person.PersonId = HumanResource.Contact.PersonId
WHERE        (Product.Customer.Status = 1) and (Product.Customer.CustomerId = @Id)
ORDER BY     Product.Customer.CustomerId


GO
/****** Object:  StoredProcedure [Product].[GetDiscountDetailForMonth]    Script Date: 23/01/2016 10:12:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetProductDetail] '78'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-29
-- Description:	Get details of Discount for month by Date of sold
-- ================================================

ALTER PROCEDURE [Product].[GetDiscountDetailForMonth]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, CONVERT(varchar, Product.SalesDetail.DateSold, 105) AS SoldOn, 
			  Product.SalesDetail.Discount, Product.SalesDetail.DiscountBy, Product.SalesDetail.DiscountAs
                         
FROM          Product.SalesDetail INNER JOIN
              Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
              Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
              Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId
WHERE        (CAST(Product.SalesDetail.DateSold AS Date) BETWEEN CAST(@DateFrom AS Date) AND CAST(@DateTo AS Date)) AND (Product.SalesDetail.Discount <> 0) 

ORDER BY Product.SalesDetail.DateSold

USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetDiscountDetailsForTheDay]    Script Date: 23/01/2016 10:12:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetSalesDetailsByOrderId] '78'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-29
-- Description:	Get details of Discount by the date
-- ================================================

ALTER PROCEDURE [Product].[GetDiscountDetailsForTheDay]
	(
		@Date datetime
	)
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, Product.SalesDetail.Quantity, Product.ProductType.StockAs, Product.SalesDetail.QuantityActual, 
              Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.SalesDetail.Price, Product.SalesDetail.DiscountBy, 
              Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice, CONVERT(varchar, Product.SalesDetail.DateSold, 105) AS SoldOn
FROM            Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date) AND ((Product.SalesDetail.Quantity != 0) OR (Product.SalesDetail.QuantityActual != 0) OR (Product.SalesDetail.QuantityLower != 0)) AND (Product.SalesDetail.Discount != 0)



USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetInvoiceById]    Script Date: 23/01/2016 10:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetInvoiceById] '10'
-- ================================================
-- Author:		@-Ahamed Zameer
-- Create date: 2015-12-17
-- Description:	Get details of product by order Id
-- ================================================
ALTER PROCEDURE [Product].[GetInvoiceById]
	(
		@Id int
	)
AS
SELECT        Product.Product.Name, Product.ProductDetail.ProductCode, Product.SalesDetailCart.Quantity, Product.ProductType.StockAs, Product.SalesDetailCart.QuantityActual, 
                         Product.Measure.Actual, Product.SalesDetailCart.QuantityLower, Product.Measure.Lower, Product.SalesDetailCart.SellingPrice, Product.SalesDetailCart.Price, Product.SalesDetailCart.DiscountBy, 
                         Product.SalesDetailCart.DiscountAs, Product.SalesDetailCart.Discount, Product.SalesDetailCart.NetPrice, Product.SalesDetailCart.DateSold, 
                         Product.SalesDetailCart.InvoiceId
FROM            Product.SalesDetailCart INNER JOIN
                         Product.ProductDetail ON Product.SalesDetailCart.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.SalesDetailCart.InvoiceId = @Id)  AND (Product.SalesDetailCart.Status = 1)

USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetLowerQuantityDatails]    Script Date: 23/01/2016 10:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of Lower quantity
-- ================================================

ALTER PROCEDURE [Product].[GetLowerQuantityDatails]
	
AS
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, Product.Product.SellingLower, Product.ProductDetail.SellingPrice, Product.Product.MarginAmount,  
              Product.ProductDetail.UnitPrice
FROM            Product.Product INNER JOIN
                Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId AND Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId
WHERE        (Product.Product.Status = 1) AND (Product.Product.SellingLower = 1)
ORDER BY     Product.Product.ProductId

USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetMeasureDatails]    Script Date: 23/01/2016 10:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of measures
-- ================================================

ALTER PROCEDURE [Product].[GetMeasureDatails]
	
AS
SELECT        MeasureId, Lower, Volume, Actual
FROM            Product.Measure
WHERE        (Product.Measure.Status = 1)

USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductDatails]    Script Date: 23/01/2016 10:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of product
-- ================================================

ALTER PROCEDURE [Product].[GetProductDatails]
	
AS
SELECT        Product.Product.Name, Product.Measure.Actual, Product.Measure.Volume, Product.Measure.Lower, Product.ProductType.StockAs, 
                         Product.Product.MarginAmount
FROM            Product.Product INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductDetail]    Script Date: 23/01/2016 10:14:44 ******/
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
              Product.ProductDetail.OpeningQuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, CONVERT(varchar, Product.ProductDetail.DateReceived, 105) AS ReceivedOn,
			  CAST(ROUND(dbo.fn_CalculateCostPrice(Product.Measure.Volume, Product.Product.ContainsQty, 
              Product.ProductDetail.UnitPrice, Product.ProductDetail.OpeningQuantity, Product.ProductDetail.OpeningQuantityActual, Product.ProductDetail.OpeningQuantityLower), 0) AS numeric(36, 2)) AS CostPrice
FROM            Product.ProductDetail INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE Cast(Product.ProductDetail.DateReceived As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductSupplierDatails]    Script Date: 23/01/2016 10:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of product supplier
-- ================================================

ALTER PROCEDURE [Product].[GetProductSupplierDatails]
	
AS
SELECT            DISTINCT Product.Product.ProductId, Product.Product.Name, Product.Vendor.Name AS Vendor, HumanResource.Person.FirstName + ' ' + HumanResource.Person.LastName AS Supplier
FROM              Product.ProductDetail INNER JOIN
                  Product.Product INNER JOIN
                  Product.Vendor ON Product.Product.VendorId = Product.Vendor.VendorId ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                  Product.Supplier ON Product.ProductDetail.SupplierId = Product.Supplier.SupplierId INNER JOIN
                  HumanResource.Person ON Product.Supplier.PersonId = HumanResource.Person.PersonId AND Product.Supplier.PersonId = HumanResource.Person.PersonId
WHERE         (Product.Product.Status = 1)
ORDER BY      Product.Product.Name


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetProductTypeDatails]    Script Date: 23/01/2016 10:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of Product Types
-- ================================================

ALTER PROCEDURE [Product].[GetProductTypeDatails]
	
AS
SELECT        Product.ProductType.ProductTypeId, Product.ProductType.Type, Product.ProductType.StockAs, Product.Measure.Actual, Product.Product.ProductId
FROM            Product.Measure INNER JOIN
                         Product.ProductType ON Product.Measure.MeasureId = Product.ProductType.MeasureId LEFT OUTER JOIN
                         Product.Product ON Product.ProductType.ProductTypeId = Product.Product.ProductTypeId
WHERE        (Product.ProductType.Status = 1)
ORDER BY      Product.ProductType.Type


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetPurchasedProductsByReferenceCode]    Script Date: 23/01/2016 10:15:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [Product].[GetProductDetail] '78'
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-12-29
-- Description:	Get details of Purchased product by Reference Code
-- ================================================

ALTER PROCEDURE [Product].[GetPurchasedProductsByReferenceCode]
	(
		@ProductName varchar(50)
	)
AS
SELECT        [dbo].[fn_GetProductDisplayInfo](Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) As ProductInfo, Product.ProductDetail.OpeningQuantity, 
              Product.ProductDetail.OpeningQuantityActual, Product.ProductDetail.OpeningQuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, CONVERT(varchar, Product.ProductDetail.DateReceived, 105) AS ReceivedOn, 
              Product.[Order].ReferenceCode, HumanResource.Person.FirstName + ' ' + HumanResource.Person.LastName AS Supplier, CAST(ROUND(dbo.fn_CalculateCostPrice(Product.Measure.Volume, Product.Product.ContainsQty, 
              Product.ProductDetail.UnitPrice, Product.ProductDetail.OpeningQuantity, Product.ProductDetail.OpeningQuantityActual, Product.ProductDetail.OpeningQuantityLower), 0) AS numeric(36, 2)) AS CostPrice
FROM            Product.ProductDetail INNER JOIN
                Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                Product.[Order] ON Product.ProductDetail.OrderId = Product.[Order].OrderId LEFT OUTER JOIN
                Product.Supplier ON Product.[Order].SupplierId = Product.Supplier.SupplierId AND Product.[Order].SupplierId = Product.Supplier.SupplierId LEFT OUTER JOIN
                HumanResource.Person ON Product.Supplier.PersonId = HumanResource.Person.PersonId
WHERE        (Product.[Order].Status = 1) AND (Product.[Order].ReferenceCode = @ProductName)


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetPurchaseOrderDatails]    Script Date: 23/01/2016 10:15:47 ******/
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
/****** Object:  StoredProcedure [Product].[GetReturnDetailsByDate]    Script Date: 23/01/2016 10:16:02 ******/
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
              Product.ProductReturn.Quantity, Product.ProductReturn.QuantityActual, Product.ProductReturn.QuantityLower, Product.ProductType.StockAs, Product.Measure.Lower, Product.Measure.Actual, CONVERT(varchar, 
              Product.SalesDetail.DateSold, 105) AS SoldOn, Product.SalesDetail.NetPrice, Product.SalesDetail.Discount, Product.ProductDetail.ProductCode
FROM            Product.Product INNER JOIN
                Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                Product.ProductReturn ON Product.ProductDetail.ProductDetailId = Product.ProductReturn.ProductDetailId INNER JOIN
                Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                Product.SalesDetail ON Product.ProductDetail.ProductDetailId = Product.SalesDetail.ProductDetailId AND Product.ProductReturn.SalesDetailId = Product.SalesDetail.SalesDetailId
WHERE        (CAST(Product.ProductReturn.DateReturned AS Date) = CAST(@Date AS Date)) AND ((Product.ProductReturn.Quantity != 0) OR (Product.ProductReturn.QuantityActual != 0) OR (Product.ProductReturn.QuantityLower != 0))
ORDER BY     Product.ProductReturn.DateReturned


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetReturnDetailsByDateRange]    Script Date: 23/01/2016 10:16:18 ******/
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
                         CONVERT(varchar, Product.SalesDetail.DateSold , 105) AS SoldOn, Product.SalesDetail.NetPrice, Product.ProductDetail.ProductCode
FROM            Product.Product INNER JOIN
                         Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
                         Product.ProductReturn ON Product.ProductDetail.ProductDetailId = Product.ProductReturn.ProductDetailId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.SalesDetail ON Product.ProductDetail.ProductDetailId = Product.SalesDetail.ProductDetailId AND Product.ProductReturn.SalesDetailId = Product.SalesDetail.SalesDetailId
WHERE Cast(Product.ProductReturn.DateReturned As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date) AND ((Product.ProductReturn.Quantity != 0) OR (Product.ProductReturn.QuantityActual != 0) OR (Product.ProductReturn.QuantityLower != 0))

ORDER BY     Product.ProductReturn.DateReturned


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailForMonth]    Script Date: 23/01/2016 10:16:36 ******/
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
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.Invoice ON Product.SalesDetail.InvoiceId = Product.Invoice.InvoiceId ON Product.Customer.CustomerId = Product.Invoice.CustomerId
WHERE Cast(Product.SalesDetail.DateSold As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date) AND ((Product.SalesDetail.Quantity != 0) OR (Product.SalesDetail.QuantityActual != 0) OR (Product.SalesDetail.QuantityLower != 0)) 

ORDER BY Product.SalesDetail.DateSold


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailsByOrderId]    Script Date: 23/01/2016 10:16:52 ******/
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
SELECT        dbo.fn_GetProductDisplayInfo(Product.ProductDetail.ProductCode, Product.Product.Name, Product.ProductType.Type) AS ProductInfo, Product.SalesDetail.Quantity, Product.ProductType.StockAs, 
                         Product.SalesDetail.QuantityActual, Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.ProductDetail.UnitPrice, Product.SalesDetail.SellingPrice, 
                         Product.SalesDetail.DiscountBy, Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice, CONVERT(varchar, Product.SalesDetail.DateSold, 105) AS SoldOn, 
                         HumanResource.Person.FirstName + ' ' + HumanResource.Person.LastName AS CustomerName, CAST(ROUND(dbo.fn_CalculateCostPrice(Product.Measure.Volume, Product.Product.ContainsQty, 
                         Product.ProductDetail.UnitPrice, Product.SalesDetail.Quantity, Product.SalesDetail.QuantityActual, Product.SalesDetail.QuantityLower), 0) AS numeric(36, 2)) AS CostPrice, Product.ProductDetail.ProductCode
FROM            HumanResource.Person INNER JOIN
                         Product.Customer ON HumanResource.Person.PersonId = Product.Customer.PersonId RIGHT OUTER JOIN
                         Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.Invoice ON Product.SalesDetail.InvoiceId = Product.Invoice.InvoiceId ON Product.Customer.CustomerId = Product.Invoice.CustomerId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date) AND ((Product.SalesDetail.Quantity != 0) OR (Product.SalesDetail.QuantityActual != 0) OR (Product.SalesDetail.QuantityLower != 0)) 


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetStockDatails]    Script Date: 23/01/2016 10:17:08 ******/
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
ORDER BY      Product.Product.Name


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetStockDatailsByProductType]    Script Date: 23/01/2016 10:17:26 ******/
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
			  Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, Product.ProductDetail.QuantityLower),0) as numeric(36,2)) As NetPrice, Product.ProductDetail.SellingPrice, CAST(ROUND(dbo.fn_CalculatePrice(Product.Measure.Volume, Product.Product.ContainsQty, Product.ProductDetail.SellingPrice, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
              Product.ProductDetail.QuantityLower), 0) AS numeric(36, 2)) AS TotalSellingPrice
FROM          Product.Product INNER JOIN
              Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
              Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
              Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE        (Product.ProductDetail.Status = 1) AND (Product.ProductType.Type = @ProductName)
ORDER BY      Product.Product.Name


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetSupplierDatails]    Script Date: 23/01/2016 10:17:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-24
-- Description:	Get details of a Supplier
-- ================================================

ALTER PROCEDURE [Product].[GetSupplierDatails]
(
		@Id varchar(50)
)
AS
SELECT        Product.Supplier.SupplierId, HumanResource.Person.FirstName, HumanResource.Person.LastName, HumanResource.Contact.LandNumber, HumanResource.Contact.MobileNumber, 
                         HumanResource.Contact.AddressNo, HumanResource.Contact.AddressStreet, HumanResource.Contact.City, HumanResource.Contact.Province
FROM            Product.Supplier INNER JOIN
                         HumanResource.Person ON Product.Supplier.PersonId = HumanResource.Person.PersonId AND Product.Supplier.PersonId = HumanResource.Person.PersonId INNER JOIN
                         HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND 
                         HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId
WHERE        (Product.Supplier.Status = 1) AND (Product.Supplier.SupplierId = @Id)
ORDER BY Product.Supplier.SupplierId


USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [Product].[GetVendorDatails]    Script Date: 23/01/2016 10:18:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of vendors
-- ================================================

ALTER PROCEDURE [Product].[GetVendorDatails]
	
AS
SELECT        VendorId, Name
FROM            Product.Vendor
WHERE        (Product.Vendor.Status = 1)

