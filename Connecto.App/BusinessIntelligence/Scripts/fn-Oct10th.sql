USE [ConnectoDb]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateCostPrice]    Script Date: 11/10/2015 13:02:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohamed Naizar
-- Create date: 2015-10-11
-- Description:	CalculateCostPrice
-- =============================================
ALTER FUNCTION [dbo].[fn_CalculateCostPrice]
(
--@volume int = 1000, @containsQty int = 50, @Costprice int = 950,
--@quanty int = 5, @purchadedAct int = 25, @purchadedLwr int = 500;
	@volume int, @containsQty int, @costprice int, @quanty int, @purchadedAct int, @purchadedLwr int
)
RETURNS float
AS
BEGIN
	declare @qtyprice float = @quanty*@costprice;
	declare @untprice float = @costprice/@containsQty;
	declare @actprice float = (@purchadedAct * @untprice);
	declare @lwrprice float = (@untprice/@volume) * @purchadedLwr;

	-- Return the result of the function
	RETURN @qtyprice + @actprice + @lwrprice
END



USE [ConnectoDb]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculatePrice]    Script Date: 11/10/2015 13:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Zameer
-- Create date: 2015-10-10
-- Description:	CalculatePrice
-- =============================================
ALTER FUNCTION [dbo].[fn_CalculatePrice]
(
--@volume int = 1000, @containsQty int = 50, @sellingprice int = 1000,
--@quanty int = 5, @purchadedAct int = 25, @purchadedLwr int = 500;
	@volume int, @containsQty int, @sellingprice int, @quanty int, @purchadedAct int, @purchadedLwr int
)
RETURNS float
AS
BEGIN
	declare @qtyprice float = @quanty*@sellingprice;
	declare @untprice float = @sellingprice/@containsQty;
	declare @actprice float = (@purchadedAct * @untprice);
	declare @lwrprice float = (@untprice/@volume) * @purchadedLwr;

	-- Return the result of the function
	RETURN @qtyprice + @actprice + @lwrprice
END



USE [ConnectoDb]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetProductDisplayInfo]    Script Date: 11/10/2015 13:04:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Zameer
-- Create date: 2015-10-10
-- Description:	GetProductInfo
-- =============================================
ALTER FUNCTION [dbo].[fn_GetProductDisplayInfo]
(
	@productcode nvarchar(300), @productname nvarchar(300), @producttype nvarchar(300)
)
RETURNS nvarchar(MAX)
AS
BEGIN
	-- Return the result of the function
	Return '[' + @productcode + '] ' +  @productname + ' - ' + @producttype
END


