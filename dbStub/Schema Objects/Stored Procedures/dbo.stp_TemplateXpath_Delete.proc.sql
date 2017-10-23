-- =============================================
-- Author:		.
-- Create date: 21-11-2007
-- Description:	Verwijder een TemplateXpath item
-- =============================================
CREATE PROCEDURE [dbo].[stp_TemplateXpath_Delete]	
	@TemplateID int,
	@XpathID int	
AS
BEGIN
	SET NOCOUNT OFF;
	DELETE FROM [dbo].[TemplateXpath]
	WHERE 
 	[TemplateID] = @TemplateID AND [XpathID] = @XpathID
	SET NOCOUNT ON;
END


