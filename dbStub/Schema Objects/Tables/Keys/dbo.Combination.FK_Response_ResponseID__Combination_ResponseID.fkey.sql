ALTER TABLE [dbo].[Combination] ADD
CONSTRAINT [FK_Response_ResponseID__Combination_ResponseID] FOREIGN KEY ([ResponseID]) REFERENCES [dbo].[Response] ([ResponseID])


