-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectCombinationByMessageType]
	@Namespace as varchar(250), 
	@Rootnode as varchar(250) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --Ophalen Combinations aan de hand van Namespace en Rootnode 
	--SELECT @CombinationID = CombinationID, @ResponseID = ResponseID
	SELECT zc.CombinationID, zc.ResponseID, xp.Expression, zcxp.XpathValue
	FROM [dbo].[Combination] zc
		INNER JOIN [dbo].[CombinationXpath] zcxp on zc.CombinationID=zcxp.CombinationID
		INNER JOIN [dbo].[Xpath] xp on xp.XpathID=zcxp.XpathID
	WHERE EXISTS
		(SELECT MessageTypeID 
		FROM [dbo].[MessageType] 
		WHERE [Namespace] = @Namespace
			AND [Rootnode] = @Rootnode
			AND zc.MessageTypeID = MessageTypeID)
END


