-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Selecteer Xpaths bij Combination
-- =============================================

CREATE PROCEDURE [dbo].[stp_SelectXpathsByCombinationID]
	@CombinationID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	SELECT [XpathID] FROM [dbo].[CombinationXpath]
	WHERE [CombinationID] = @CombinationID
	
	SET NOCOUNT OFF;
END


