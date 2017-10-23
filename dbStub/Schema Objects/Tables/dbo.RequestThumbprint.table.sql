CREATE TABLE [dbo].[RequestThumbprint]
(
[RequestThumbPrintID]  [int] NOT NULL IDENTITY(1, 1),
[ResponseID] [int] NOT NULL,
[RequestID] [int] NOT NULL,
[Thumbprint] [char] (40) NOT NULL
) ON [PRIMARY]


