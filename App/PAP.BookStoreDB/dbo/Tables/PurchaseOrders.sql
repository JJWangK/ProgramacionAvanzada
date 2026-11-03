CREATE TABLE [dbo].[PurchaseOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ExpectedDate] [date] NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalCost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PurchaseOrders] ADD CONSTRAINT [DF_PurchaseOrders_OrderDate] DEFAULT (sysutcdatetime()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[PurchaseOrders] ADD CONSTRAINT [DF_PurchaseOrders_Status] DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[PurchaseOrders] ADD CONSTRAINT [DF_PurchaseOrders_TotalCost] DEFAULT ((0)) FOR [TotalCost]
GO
ALTER TABLE [dbo].[PurchaseOrders] ADD CONSTRAINT [FK_PurchaseOrders_Suppliers] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_Suppliers]
