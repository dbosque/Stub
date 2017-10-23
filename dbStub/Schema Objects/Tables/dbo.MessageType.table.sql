CREATE TABLE [dbo].[MessageType](
	[MessageTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Namespace] [varchar](250) NOT NULL,
	[Rootnode] [varchar](250) NOT NULL,
	[Description] [varchar](250) NULL,
	[PassthroughEnabled] [bit] NOT NULL CONSTRAINT [DF_MessageType_PassthroughEnabled]  DEFAULT ((0)),
	[PassthroughUrl] [varchar](max) NULL,
	[Sample] [varchar](max) NULL
) ON [PRIMARY]