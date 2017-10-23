-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Verwijder Xpath met ID
-- =============================================

CREATE PROCEDURE [dbo].[stp_RemoveXpathByID]
	@XpathID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	DELETE FROM [dbo].[TemplateXpath]
	WHERE [XpathID] = @XpathID
	
	DELETE FROM [dbo].[CombinationXpath]
	WHERE [XpathID] = @XpathID
	
	DELETE FROM [dbo].[Xpath]
	WHERE [XpathID] = @XpathID
	
	SET NOCOUNT OFF;
END


