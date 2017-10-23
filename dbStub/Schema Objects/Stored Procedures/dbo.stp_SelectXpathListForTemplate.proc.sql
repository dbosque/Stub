-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectXpathListForTemplate] 
	@Template_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	select distinct XPath.XPathId, XPath.Description, XPath.Expression  from XPath
	join TemplateXPath on TemplateXPath.XpathID = Xpath.XpathId
	join Template on Template.TemplateId = TemplateXPath.TemplateId	
	where Template.TemplateID = @Template_id
END


