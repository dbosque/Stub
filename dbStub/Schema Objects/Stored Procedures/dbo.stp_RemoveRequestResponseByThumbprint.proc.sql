-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_RemoveRequestResponseByThumbprint]
	@thumbprint varchar(40)
AS
BEGIN
	declare @request_id int
	declare @reponse_id int

	select @request_id = RequestID, @reponse_id = ResponseID from RequestthumbPrint
	where Thumbprint = @thumbprint

	delete from RequestthumbPrint 
	where Thumbprint = @thumbprint

	delete from request where RequestID = @request_id

	delete from response where ResponseID = @reponse_id
	and not exists ( select ResponseID from Combination
					 where ResponseID = @reponse_id )

END


