ALTER TABLE [dbo].[CombinationXpath] ADD
CONSTRAINT [FK_Combination_CombinationID__CombinationXpath_CombinationID] FOREIGN KEY ([CombinationID]) REFERENCES [dbo].[Combination] ([CombinationID])


