-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectTemplatesForMessageType]
	@rootnode varchar(250),
	@namespace varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select distinct Template.Description,Template.TemplateID,MessageType.MessageTypeId from messagetype
	join Template on Template.MessageTypeId = MessageType.MessageTypeId
	join TemplateXPath on TemplateXPath.TemplateId = Template.TemplateId
	join XPath on TemplateXPath.XPathId= TemplateXPath.XpathId
	where MessageType.RootNode = @rootnode and MessageType.Namespace = @namespace
END


