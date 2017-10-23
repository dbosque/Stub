ALTER TABLE [dbo].[RequestThumbprint] ADD
CONSTRAINT [FK_Response_ResponseID__RequestThumbprint_ResponseID] FOREIGN KEY ([ResponseID]) REFERENCES [dbo].[Response] ([ResponseID])


