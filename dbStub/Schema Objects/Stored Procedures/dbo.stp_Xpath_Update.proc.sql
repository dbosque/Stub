-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Update Xpath item
-- =============================================
CREATE PROCEDURE [dbo].[stp_Xpath_Update]
	@XpathID int,
	@Expression varchar(1000),
	@Description varchar(250)
	
AS
BEGIN	

	SET NOCOUNT OFF;
	UPDATE [dbo].[XPath] SET
		[Expression] = @Expression,
		[Description] = @Description
	WHERE XpathID = @XpathID
	;	
END


