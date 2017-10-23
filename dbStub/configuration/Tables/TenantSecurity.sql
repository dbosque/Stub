CREATE TABLE [configuration].[TenantSecurity](
	[TenantSecurityId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[SecurityCode] [uniqueidentifier] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_TenantSecurity] PRIMARY KEY CLUSTERED 
(
	[TenantSecurityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [configuration].[TenantSecurity]  WITH CHECK ADD  CONSTRAINT [FK_TenantSecurity_Tenant] FOREIGN KEY([TenantId])
REFERENCES [configuration].[Tenant] ([TenantId])
GO

ALTER TABLE [configuration].[TenantSecurity] CHECK CONSTRAINT [FK_TenantSecurity_Tenant]