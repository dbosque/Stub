CREATE TABLE [configuration].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [configuration].[Settings]  WITH CHECK ADD  CONSTRAINT [FK_Settings_Tenant] FOREIGN KEY([TenantId])
REFERENCES [configuration].[Tenant] ([TenantId])
GO

ALTER TABLE [configuration].[Settings] CHECK CONSTRAINT [FK_Settings_Tenant]