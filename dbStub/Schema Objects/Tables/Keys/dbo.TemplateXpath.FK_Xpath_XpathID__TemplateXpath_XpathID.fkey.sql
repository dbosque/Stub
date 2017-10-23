ALTER TABLE [dbo].[TemplateXpath] ADD
CONSTRAINT [FK_Xpath_XpathID__TemplateXpath_XpathID] FOREIGN KEY ([XpathID]) REFERENCES [dbo].[Xpath] ([XpathID])


