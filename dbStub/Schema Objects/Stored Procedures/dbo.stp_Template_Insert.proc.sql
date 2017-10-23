-- =============================================
-- Author:		.
-- Create date: 21-11-2007
-- Description:	Voeg een nieuwe Template item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_Template_Insert]	
	@MessageTypeID int,
	@Description varchar(250)
	
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[Template] (
		[MessageTypeID], [Description]
	) VALUES (
		@MessageTypeID, @Description
	);
	SET NOCOUNT ON;
	SELECT
		[TemplateID],
		[MessageTypeID],
		[Description]
	FROM
		[dbo].[Template]
	WHERE
		(TemplateID = SCOPE_IDENTITY());

END


