-- =============================================
-- Author:		.
-- Create date: 21-11-2007
-- Description:	Voeg een TemplateXpath item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_TemplateXpath_Insert]	
	@TemplateID int,
	@XpathID int
	
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[TemplateXpath] (
		[TemplateID], [XpathID]
	) VALUES (
		@TemplateID, @XpathID
	);
	SET NOCOUNT ON;
	SELECT
		[TemplateXpathID],
		[TemplateID],
		[XpathID]
	FROM
		[dbo].[TemplateXpath]
	WHERE
		(TemplateXpathID = SCOPE_IDENTITY());

END


