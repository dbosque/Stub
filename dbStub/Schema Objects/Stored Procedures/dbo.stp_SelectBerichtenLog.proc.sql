-- =============================================
-- Author:		.
-- Create date: 19 November 2007
-- Description:	Selecteer een gedeelte van de berichtenlog
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectBerichtenLog]
	@MagRegels as int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP(@MagRegels)* FROM viewBerichtenLog
	ORDER BY ResponseDatumTijd DESC
END


