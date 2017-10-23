ALTER TABLE [dbo].[RequestThumbprint] ADD
CONSTRAINT [FK_Request_RequestID__RequestThumbprint_Request] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[Request] ([RequestID])


