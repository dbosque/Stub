ALTER TABLE [dbo].[Template] ADD
CONSTRAINT [FK_MessageType_MessageTypeID__Template_MessageTypeID] FOREIGN KEY ([MessageTypeID]) REFERENCES [dbo].[MessageType] ([MessageTypeID])


