ALTER TABLE [dbo].[Combination] ADD
CONSTRAINT [FK_Template_TemplateID__Combination_TemplateID] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Template] ([TemplateID])


