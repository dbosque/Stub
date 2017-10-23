-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Verwijder Combination Met Bijbehorende Response
-- =============================================

CREATE PROCEDURE [dbo].[stp_RemoveCombinationWithResponse]
	@CombinationID int,
	@RespID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	SELECT ResponseID FROM [dbo].[Combination]
	WHERE [CombinationID] = @CombinationID AND ResponseID = @RespID
	
	DELETE FROM [dbo].[CombinationXpath]
	WHERE [CombinationID] = @CombinationID
	
	DELETE FROM [dbo].[Combination]
	WHERE [CombinationID] = @CombinationID
	
	DELETE FROM [dbo].[Response]
	WHERE [ResponseID] = @RespID
	
	SET NOCOUNT OFF;
END


