CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ShippingAddressId] [int] NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Subtotal] [decimal](18, 2) NOT NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[ShippingCost] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Notes] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [DF_Orders_OrderDate] DEFAULT (sysutcdatetime()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [DF_Orders_Status] DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [DF_Orders_TaxAmount] DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [DF_Orders_ShippingCost] DEFAULT ((0)) FOR [ShippingCost]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [DF_Orders_UpdatedAt] DEFAULT (sysutcdatetime()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Addresses] FOREIGN KEY([ShippingAddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Addresses]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
