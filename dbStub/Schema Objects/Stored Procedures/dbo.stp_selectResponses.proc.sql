-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_selectResponses] @messagetypeid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select messagetype.Description, xpath.Description, XpathValue, * from Response
	join combination on combination.responseid = response.responseid
	join messagetype on messagetype.messagetypeid = combination.messagetypeid
	join combinationxpath on combinationxpath.combinationid = combination.combinationid
	join xpath on xpath.xpathid = combinationxpath.xpathid
	where combination.messagetypeid = @messagetypeid
END


