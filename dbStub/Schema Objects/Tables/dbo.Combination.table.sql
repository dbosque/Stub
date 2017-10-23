CREATE TABLE [dbo].[Combination]
(
[CombinationID] [int] NOT NULL IDENTITY(1, 1),
[MessageTypeID] [int] NOT NULL,
[TemplateID] [int] NOT NULL,
[ResponseID] [int] NOT NULL,
[Description] [varchar] (250) NULL
) ON [PRIMARY]


