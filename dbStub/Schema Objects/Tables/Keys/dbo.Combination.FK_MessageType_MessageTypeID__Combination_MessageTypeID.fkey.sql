ALTER TABLE [dbo].[Combination] ADD
CONSTRAINT [FK_MessageType_MessageTypeID__Combination_MessageTypeID] FOREIGN KEY ([MessageTypeID]) REFERENCES [dbo].[MessageType] ([MessageTypeID])


