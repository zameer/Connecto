USE [ConnectoDb]
GO
/****** Object:  Table [Connecto].[ReportSetting]    Script Date: 5/18/2015 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Connecto].[ReportSetting](
	[ReportGuid] [uniqueidentifier] NOT NULL,
	[ReportName] [nvarchar](max) NULL,
	[ReportTitle] [nvarchar](max) NULL,
	[CommandText] [nvarchar](max) NULL,
	[ReportPath] [nvarchar](max) NULL,
	[Parameters] [nvarchar](max) NULL,
 CONSTRAINT [PK_Connecto.ReportSetting] PRIMARY KEY CLUSTERED 
(
	[ReportGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [Connecto].[ReportSetting] ([ReportGuid], [ReportName], [ReportTitle], [CommandText], [ReportPath], [Parameters]) VALUES (N'8d64a1f1-7482-4376-b64d-2c8db0683c95', N'Sales Details', N'Sales Details', N'[Product].[GetSalesDetailsByOrderId]', N'Transaction/SalesDetailsByOrderId.rdlc', N'Id')
INSERT [Connecto].[ReportSetting] ([ReportGuid], [ReportName], [ReportTitle], [CommandText], [ReportPath], [Parameters]) VALUES (N'a88a2fcb-d4e3-491a-b220-74e6ae5cec9f', N'Purchasing Details', N'Purchasing Details', N'[Product].[GetProductDetail]', N'Transaction/ProductDetails.rdlc', N'Id')
