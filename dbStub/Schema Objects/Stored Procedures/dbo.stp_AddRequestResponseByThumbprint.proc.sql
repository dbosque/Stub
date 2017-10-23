-- =============================================
-- Author:		.
-- Create date: 19-11-2007
-- Description:	Voeg een nieuwe request/reponse item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_AddRequestResponseByThumbprint]
	@request varchar(MAX),
	@requestdescription varchar(250),
	@response varchar(MAX),
	@responsedescription varchar(250),
	@thumbprint varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @response_id int
	DECLARE @request_id int
	

	BEGIN TRY
	   BEGIN TRAN;
	   -- voeg de request en response items toe
	   INSERT Request(Request,Description) VALUES(@request, @requestdescription)
	   SET @request_id = SCOPE_IDENTITY()
	   
	   INSERT Response(ResponseText,Description) VALUES(@response, @responsedescription)
	   SET @response_id = SCOPE_IDENTITY() 
	   
	   -- voeg de combina toe 
	   INSERT RequestThumbPrint(ResponseID,RequestID,thumbprint) VALUES(@response_id, @request_id,@thumbprint)
	   COMMIT;
	END TRY
	BEGIN CATCH
	   IF XACT_STATE() <> 0
	   BEGIN
		  ROLLBACK;
	   END;

	   --SELECT ERROR_NUMBER()  AS ErrorNumber
	   --	 ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH;

END


