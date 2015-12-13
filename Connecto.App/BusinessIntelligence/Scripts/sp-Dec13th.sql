USE [MinaDb]
GO
/****** Object:  StoredProcedure [HumanResource].[GetAllEmployeeDatails]    Script Date: 13/12/2015 15:38:17 ******/
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
/****** Object:  StoredProcedure [HumanResource].[GetEmployeeDatails]    Script Date: 13/12/2015 15:38:55 ******/
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
/****** Object:  StoredProcedure [Product].[GetAllCustomerDatails]    Script Date: 13/12/2015 15:39:13 ******/
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
/****** Object:  StoredProcedure [Product].[GetAllSupplierDatails]    Script Date: 13/12/2015 15:39:31 ******/
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
SELECT       Product.Supplier.SupplierId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         HumanResource.Person INNER JOIN
             HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId INNER JOIN
             Product.Supplier ON HumanResource.Person.PersonId = Product.Supplier.PersonId AND HumanResource.Person.PersonId = Product.Supplier.PersonId
WHERE        (Product.Supplier.Status = 1)
ORDER BY     Product.Supplier.SupplierId


GO
/****** Object:  StoredProcedure [Product].[GetCustomerDatails]    Script Date: 13/12/2015 15:39:48 ******/
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
/****** Object:  StoredProcedure [Product].[GetDiscountDetailForMonth]    Script Date: 13/12/2015 15:40:05 ******/
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
CREATE PROCEDURE [Product].[GetDiscountDetailForMonth]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        ProductCode, DateSold, Discount, DiscountBy, DiscountAs
FROM            Product.SalesDetail

WHERE Cast(Product.SalesDetail.DateSold As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)

ORDER BY Product.SalesDetail.DateSold


GO
/****** Object:  StoredProcedure [Product].[GetDiscountDetailsForTheDay]    Script Date: 13/12/2015 15:40:21 ******/
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
CREATE PROCEDURE [Product].[GetDiscountDetailsForTheDay]
	(
		@Date datetime
	)
AS
SELECT        Product.Product.Name, Product.ProductDetail.ProductCode, Product.SalesDetail.Quantity, Product.ProductType.StockAs, Product.SalesDetail.QuantityActual, 
                         Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.SalesDetail.Price, Product.SalesDetail.DiscountBy, 
                         Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice, Product.SalesDetail.DateSold
FROM            Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date)


GO
/****** Object:  StoredProcedure [Product].[GetLowerQuantityDatails]    Script Date: 13/12/2015 15:40:37 ******/
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
SELECT       Product.Product.ProductId, Product.ProductDetail.ProductCode, Product.Product.Name, Product.Product.SellingLower, Product.ProductDetail.SellingPrice, Product.Product.MarginAmount
FROM         Product.Product INNER JOIN
             Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId AND Product.Product.ProductId = Product.ProductDetail.ProductId
WHERE        (Product.Product.Status = 1) AND (Product.Product.SellingLower = 1)
ORDER BY     Product.Product.ProductId



GO
/****** Object:  StoredProcedure [Product].[GetMeasureDatails]    Script Date: 13/12/2015 15:40:53 ******/
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



GO
/****** Object:  StoredProcedure [Product].[GetProductDatails]    Script Date: 13/12/2015 15:41:10 ******/
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


GO
/****** Object:  StoredProcedure [Product].[GetProductDetail]    Script Date: 13/12/2015 15:41:47 ******/
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


GO
/****** Object:  StoredProcedure [Product].[GetProductSupplierDatails]    Script Date: 13/12/2015 15:42:09 ******/
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
SELECT            Product.Product.ProductId, Product.Product.Name, Product.Vendor.Name AS Vendor, HumanResource.Person.FirstName AS Supplier
FROM              Product.ProductDetail INNER JOIN
                  Product.Product INNER JOIN
                  Product.Vendor ON Product.Product.VendorId = Product.Vendor.VendorId ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                  Product.Supplier ON Product.ProductDetail.SupplierId = Product.Supplier.SupplierId INNER JOIN
                  HumanResource.Person ON Product.Supplier.PersonId = HumanResource.Person.PersonId AND Product.Supplier.PersonId = HumanResource.Person.PersonId
WHERE         (Product.Product.Status = 1)
ORDER BY      Product.Product.ProductId



GO
/****** Object:  StoredProcedure [Product].[GetProductTypeDatails]    Script Date: 13/12/2015 15:42:25 ******/
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
SELECT        Product.ProductType.ProductTypeId, Product.ProductType.Type, Product.ProductType.StockAs, Product.Measure.Actual
FROM          Product.Measure INNER JOIN
              Product.ProductType ON Product.Measure.MeasureId = Product.ProductType.MeasureId



GO
/****** Object:  StoredProcedure [Product].[GetPurchaseOrderDatails]    Script Date: 13/12/2015 15:42:45 ******/
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



GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailForMonth]    Script Date: 13/12/2015 15:43:09 ******/
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
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)

ORDER BY Product.SalesDetail.DateSold



GO
/****** Object:  StoredProcedure [Product].[GetSalesDetailsByOrderId]    Script Date: 13/12/2015 15:43:27 ******/
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
                         Product.ProductDetail.UnitPrice, Product.SalesDetail.Quantity, Product.SalesDetail.QuantityActual, Product.SalesDetail.QuantityLower), 0) AS numeric(36, 2)) AS CostPrice
FROM            HumanResource.Person INNER JOIN
                         Product.Customer ON HumanResource.Person.PersonId = Product.Customer.PersonId RIGHT OUTER JOIN
                         Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId INNER JOIN
                         Product.Invoice ON Product.SalesDetail.InvoiceId = Product.Invoice.InvoiceId ON Product.Customer.CustomerId = Product.Invoice.CustomerId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date)



GO
/****** Object:  StoredProcedure [Product].[GetStockDatails]    Script Date: 13/12/2015 15:43:44 ******/
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



GO
/****** Object:  StoredProcedure [Product].[GetSupplierDatails]    Script Date: 13/12/2015 15:44:01 ******/
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



GO
/****** Object:  StoredProcedure [Product].[GetVendorDatails]    Script Date: 13/12/2015 15:44:15 ******/
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

ALTER PROCEDURE [Product].[GetProductDetail]
	(
		@DateFrom datetime,
		@DateTo datetime
	)
AS
SELECT        Product.ProductDetail.OrderId, Product.Product.Name, Product.ProductDetail.ProductCode, Product.ProductDetail.Quantity, Product.ProductDetail.QuantityActual, 
                         Product.ProductDetail.QuantityLower, Product.ProductType.StockAs, Product.Measure.Actual, Product.Measure.Lower, Product.ProductDetail.DateReceived
FROM            Product.ProductDetail INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
WHERE Cast(Product.ProductDetail.DateReceived As Date) 
Between Cast(@DateFrom As Date) And  Cast(@DateTo As Date)


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
ALTER PROCEDURE [Product].[GetSalesDetailsByOrderId]
	(
		@Date datetime
	)
AS
SELECT        Product.Product.Name, Product.ProductDetail.ProductCode, Product.SalesDetail.Quantity, Product.ProductType.StockAs, Product.SalesDetail.QuantityActual, 
                         Product.Measure.Actual, Product.SalesDetail.QuantityLower, Product.Measure.Lower, Product.SalesDetail.Price, Product.SalesDetail.DiscountBy, 
                         Product.SalesDetail.DiscountAs, Product.SalesDetail.Discount, Product.SalesDetail.NetPrice, Product.SalesDetail.DateSold
FROM            Product.SalesDetail INNER JOIN
                         Product.ProductDetail ON Product.SalesDetail.ProductDetailId = Product.ProductDetail.ProductDetailId INNER JOIN
                         Product.Product ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
Where Cast(Product.SalesDetail.DateSold As Date) = Cast(@Date As Date)
GO