-- =============================================
-- Author:		.
-- Create date: 19-11-2007
-- Description:	Stored procedure voor het ophalen van een Responsebericht
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectResponseByResponseID] 
	-- Add the parameters for the stored procedure here
	@ResponseID as int,
	@CombinationID as int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Ophalen gevonden Response
	SELECT ResponseText FROM [dbo].[Response]
	WHERE ResponseID=@ResponseID
			
	--Logrecord wegschrijven
	INSERT INTO [dbo].[StubLog]
			   ([CombinationID]           
			   ,[ResponseDatumTijd])
		 VALUES
			   (@CombinationID
			   ,getdate())
		
END


