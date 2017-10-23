ALTER TABLE [dbo].[CombinationXpath] ADD
CONSTRAINT [FK_Xpath_XpathID__CombinationXpath_XpathID] FOREIGN KEY ([XpathID]) REFERENCES [dbo].[Xpath] ([XpathID])


