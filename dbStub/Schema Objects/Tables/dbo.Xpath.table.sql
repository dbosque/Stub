CREATE TABLE [dbo].[Xpath]
(
[XpathID] [int] NOT NULL IDENTITY(1, 1),
[Expression] [varchar] (1000) NOT NULL,
[Description] [varchar] (250) NULL, 
[Type] INT NULL DEFAULT 0
) ON [PRIMARY]


