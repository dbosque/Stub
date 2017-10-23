	CREATE TABLE [dbo].[StubLog]
	(
	[StubLogID] [int] IDENTITY(1,1) NOT NULL,
		[CombinationID] [int] NULL,
		[ResponseDatumTijd] [datetime] NOT NULL,
		[TenantID] [int] NULL,
		[Request] [nvarchar](max) NOT NULL	,
		[Uri] [nvarchar](max) NULL, 
    [MessageTypeId] INT NULL
	) ON [PRIMARY]


