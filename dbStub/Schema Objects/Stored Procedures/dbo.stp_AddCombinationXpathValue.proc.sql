-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_AddCombinationXpathValue]
	@CombinationID int,
	@XpathID int,
	@XpathValue varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert CombinationXPath(	XpathID, CombinationID, XpathValue )
    values (@XpathID, @CombinationID, @XpathValue)
END


