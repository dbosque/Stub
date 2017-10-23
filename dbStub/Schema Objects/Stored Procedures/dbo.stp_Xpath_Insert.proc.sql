-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Voeg een nieuwe Xpath item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_Xpath_Insert]	
	@Expression varchar(1000),
	@Description varchar(250)
	
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[XPath] (
		[Expression], [Description]
	) VALUES (
		@Expression, @Description
	);
	SET NOCOUNT ON;
	SELECT
		[XpathID],
		[Expression],
		[Description]
	FROM
		[dbo].[XPath]
	WHERE
		(XpathID = SCOPE_IDENTITY());

END


