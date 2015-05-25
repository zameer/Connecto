USE [ConnectoDb]
GO
/****** Object:  StoredProcedure [HumanResource].[GetAllEmployeeDatails]    Script Date: 25/05/2015 17:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of All Employee
-- ================================================

CREATE PROCEDURE [HumanResource].[GetAllEmployeeDatails]

AS
SELECT       HumanResource.Employee.EmployeeId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         HumanResource.Person INNER JOIN
             HumanResource.Employee ON HumanResource.Person.PersonId = HumanResource.Employee.PersonId
WHERE        (HumanResource.Employee.Status = 1) 
ORDER BY     HumanResource.Employee.EmployeeId
/****** Object:  StoredProcedure [HumanResource].[GetEmployeeDatails]    Script Date: 25/05/2015 17:16:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-24
-- Description:	Get details of an Employee
-- ================================================

CREATE PROCEDURE [HumanResource].[GetEmployeeDatails]
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
/****** Object:  StoredProcedure [Product].[GetAllCustomerDatails]    Script Date: 25/05/2015 17:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of All customers
-- ================================================

CREATE PROCEDURE [Product].[GetAllCustomerDatails]

AS
SELECT       Product.Customer.CustomerId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         Product.Customer INNER JOIN
             HumanResource.Person ON Product.Customer.PersonId = HumanResource.Person.PersonId
WHERE        (Product.Customer.Status = 1) 
ORDER BY     Product.Customer.CustomerId
/****** Object:  StoredProcedure [Product].[GetAllSupplierDatails]    Script Date: 25/05/2015 17:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of All Supplier
-- ================================================

CREATE PROCEDURE [Product].[GetAllSupplierDatails]

AS
SELECT       Product.Supplier.SupplierId, HumanResource.Person.FirstName, HumanResource.Person.LastName
FROM         HumanResource.Person INNER JOIN
             HumanResource.Contact ON HumanResource.Person.PersonId = HumanResource.Contact.PersonId AND HumanResource.Person.PersonId = HumanResource.Contact.PersonId INNER JOIN
             Product.Supplier ON HumanResource.Person.PersonId = Product.Supplier.PersonId AND HumanResource.Person.PersonId = Product.Supplier.PersonId
WHERE        (Product.Supplier.Status = 1)
ORDER BY     Product.Supplier.SupplierId
/****** Object:  StoredProcedure [Product].[GetCustomerDatails]    Script Date: 25/05/2015 17:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of a customer
-- ================================================

CREATE PROCEDURE [Product].[GetCustomerDatails]
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
/****** Object:  StoredProcedure [Product].[GetLowerQuantityDatails]    Script Date: 25/05/2015 17:18:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of Lower quantity
-- ================================================

CREATE PROCEDURE [Product].[GetLowerQuantityDatails]
	
AS
SELECT       Product.Product.ProductId, Product.ProductDetail.ProductCode, Product.Product.Name, Product.Product.SellingLower, Product.ProductDetail.SellingPrice, Product.Product.MarginAmount
FROM         Product.Product INNER JOIN
             Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId AND Product.Product.ProductId = Product.ProductDetail.ProductId
WHERE        (Product.Product.Status = 1) AND (Product.Product.SellingLower = 1)
ORDER BY     Product.Product.ProductId
/****** Object:  StoredProcedure [Product].[GetMeasureDatails]    Script Date: 25/05/2015 17:19:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of measures
-- ================================================

CREATE PROCEDURE [Product].[GetMeasureDatails]
	
AS
SELECT        MeasureId, Lower, Volume, Actual
FROM            Product.Measure
WHERE        (Product.Measure.Status = 1)
/****** Object:  StoredProcedure [Product].[GetProductDatails]    Script Date: 25/05/2015 17:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-17
-- Description:	Get details of product
-- ================================================

CREATE PROCEDURE [Product].[GetProductDatails]
	
AS
SELECT        Product.Product.Name, Product.Measure.Actual, Product.Measure.Volume, Product.Measure.Lower, Product.ProductType.StockAs, 
                         Product.Product.MarginAmount
FROM            Product.Product INNER JOIN
                         Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId INNER JOIN
                         Product.Measure ON Product.ProductType.MeasureId = Product.Measure.MeasureId
/****** Object:  StoredProcedure [Product].[GetProductSupplierDatails]    Script Date: 25/05/2015 17:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of product supplier
-- ================================================

CREATE PROCEDURE [Product].[GetProductSupplierDatails]
	
AS
SELECT            Product.Product.ProductId, Product.Product.Name, Product.Vendor.Name AS Vendor, HumanResource.Person.FirstName AS Supplier
FROM              Product.ProductDetail INNER JOIN
                  Product.Product INNER JOIN
                  Product.Vendor ON Product.Product.VendorId = Product.Vendor.VendorId ON Product.ProductDetail.ProductId = Product.Product.ProductId INNER JOIN
                  Product.Supplier ON Product.ProductDetail.SupplierId = Product.Supplier.SupplierId INNER JOIN
                  HumanResource.Person ON Product.Supplier.PersonId = HumanResource.Person.PersonId AND Product.Supplier.PersonId = HumanResource.Person.PersonId
WHERE         (Product.Product.Status = 1)
ORDER BY      Product.Product.ProductId
/****** Object:  StoredProcedure [Product].[GetProductTypeDatails]    Script Date: 25/05/2015 17:21:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of Product Types
-- ================================================

CREATE PROCEDURE [Product].[GetProductTypeDatails]
	
AS
SELECT        Product.ProductType.ProductTypeId, Product.ProductType.Type, Product.ProductType.StockAs, Product.Measure.Actual
FROM          Product.Measure INNER JOIN
              Product.ProductType ON Product.Measure.MeasureId = Product.ProductType.MeasureId
/****** Object:  StoredProcedure [Product].[GetStockDatails]    Script Date: 25/05/2015 17:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-22
-- Description:	Get details of Stock
-- ================================================

CREATE PROCEDURE [Product].[GetStockDatails]

AS
SELECT        Product.Product.ProductId, Product.ProductDetail.ProductCode, Product.Product.Name, Product.Product.StockInHand, Product.Product.Reorderlevel, Product.ProductDetail.UnitPrice, 
              Product.ProductDetail.SellingPrice, Product.ProductDetail.DateReceived, Product.ProductType.Type
FROM          Product.Product INNER JOIN
              Product.ProductDetail ON Product.Product.ProductId = Product.ProductDetail.ProductId INNER JOIN
              Product.ProductType ON Product.Product.ProductTypeId = Product.ProductType.ProductTypeId
WHERE        (Product.Product.Status = 1)
ORDER BY      Product.Product.ProductId
/****** Object:  StoredProcedure [Product].[GetSupplierDatails]    Script Date: 25/05/2015 17:23:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-24
-- Description:	Get details of a Supplier
-- ================================================

CREATE PROCEDURE [Product].[GetSupplierDatails]
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
/****** Object:  StoredProcedure [Product].[GetVendorDatails]    Script Date: 25/05/2015 17:23:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		@-Mohamed Naizar
-- Create date: 2015-05-23
-- Description:	Get details of vendors
-- ================================================

CREATE PROCEDURE [Product].[GetVendorDatails]
	
AS
SELECT        VendorId, Name
FROM            Product.Vendor
WHERE        (Product.Vendor.Status = 1)
GO