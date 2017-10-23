CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestThumbprint] ON [dbo].[RequestThumbprint] ([Thumbprint]) 

GO
EXEC sp_addextendedproperty N'MS_Description', N'Unieke thumbprint', 'SCHEMA', N'dbo', 'TABLE', N'RequestThumbprint', 'INDEX', N'IX_RequestThumbprint'

