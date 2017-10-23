-- =============================================
-- Author:		.
-- Create date: 21-11-2007
-- Description:	Voeg een nieuwe MessageType item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_MessageType_Insert]	
	@Namespace varchar(250),	
	@Rootnode varchar(250),
	@Description varchar(250)
	
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[MessageType] (
		[Namespace], [Rootnode], [Description]
	) VALUES (
		@Namespace,@Rootnode, @Description
	);
	SET NOCOUNT ON;
	SELECT
		[MessageTypeID],
		[Namespace],
		[Rootnode],
		[Description]
	FROM
		[dbo].[MessageType]
	WHERE
		(MessageTypeID = SCOPE_IDENTITY());

END


