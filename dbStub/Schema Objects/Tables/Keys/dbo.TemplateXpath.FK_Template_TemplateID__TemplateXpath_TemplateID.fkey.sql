ALTER TABLE [dbo].[TemplateXpath] ADD
CONSTRAINT [FK_Template_TemplateID__TemplateXpath_TemplateID] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Template] ([TemplateID])


