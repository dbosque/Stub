CREATE TABLE [dbo].[Request]
(
[RequestID] [int] NOT NULL IDENTITY(1, 1),
[Request] [varchar] (max) NOT NULL,
[Description] [varchar] (250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


