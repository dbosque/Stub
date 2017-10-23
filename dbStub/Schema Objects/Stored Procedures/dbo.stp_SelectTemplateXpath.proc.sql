-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Stored procedure voor het ophalen van een Responsebericht
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectTemplateXpath] 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT MessageTypeID, [Namespace], Rootnode, Description 
	FROM [dbo].[MessageType]

	SELECT TemplateID, MessageTypeID, Description 
	FROM [dbo].[Template]

	SELECT XpathID, Expression, Description 
	FROM [dbo].[Xpath]
	
	SELECT TemplateXpathID, TemplateID, XpathID 
	FROM [dbo].[TemplateXpath]
						
END


