CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(17,1) NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Category] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Publisher] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Author] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PublicationDate] [date] NULL,
	[Height] [decimal](10, 2) NULL,
	[Width] [decimal](10, 2) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[IsBestSeller] [bit] NOT NULL,
	[Genre] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsInStock] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[PublisherId] [int] NULL,
	[CategoryId] [int] NULL,
	[GenreId] [int] NULL,
	[ISBN] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pages] [int] NULL,
	[Language] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CoverImageUrl] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Books_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Books_ISBN] ON [dbo].[Books]
(
	[ISBN] ASC
)
WHERE ([ISBN] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [DF__Books__IsBestSel__4AB81AF0]  DEFAULT ((0)) FOR [IsBestSeller]
GO
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [DF__Books__IsInStock__4BAC3F29]  DEFAULT ((1)) FOR [IsInStock]
GO
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_Books_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories]
GO
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_Books_Genres] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Genres]
GO
ALTER TABLE [dbo].[Books] ADD CONSTRAINT [FK_Books_Publishers] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publishers] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publishers]
