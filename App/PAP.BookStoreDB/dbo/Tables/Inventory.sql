CREATE TABLE [dbo].[Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[LastRestockedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Inventory_Book] UNIQUE NONCLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Inventory] ADD CONSTRAINT [DF_Inventory_QuantityInStock] DEFAULT ((0)) FOR [QuantityInStock]
GO
ALTER TABLE [dbo].[Inventory] ADD CONSTRAINT [DF_Inventory_ReorderLevel] DEFAULT ((5)) FOR [ReorderLevel]
GO
ALTER TABLE [dbo].[Inventory] ADD CONSTRAINT [FK_Inventory_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Books]
GO
ALTER TABLE [dbo].[Inventory] ADD CONSTRAINT [CK_Inventory_QuantityInStock] CHECK (([QuantityInStock]>=(0)))
