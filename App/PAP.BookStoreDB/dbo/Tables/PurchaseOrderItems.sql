CREATE TABLE [dbo].[PurchaseOrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitCost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PurchaseOrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PurchaseOrderItems] ADD CONSTRAINT [FK_POItems_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderItems] CHECK CONSTRAINT [FK_POItems_Books]
GO
ALTER TABLE [dbo].[PurchaseOrderItems] ADD CONSTRAINT [FK_POItems_PurchaseOrders] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrders] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderItems] CHECK CONSTRAINT [FK_POItems_PurchaseOrders]
GO
ALTER TABLE [dbo].[PurchaseOrderItems] ADD CONSTRAINT [CK_PurchaseOrderItems_Quantity] CHECK (([Quantity]>(0)))
