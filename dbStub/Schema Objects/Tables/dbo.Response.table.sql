CREATE TABLE [dbo].[Response]
(
[ResponseID] [int] NOT NULL IDENTITY(1, 1),
[ResponseText] [varchar] (max) NOT NULL,
[Description] [varchar] (250) NULL, 
    [StatusCode] INT NULL, 
    [ContentType] VARCHAR(250) NULL, 
    [Headers] VARCHAR(MAX) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


