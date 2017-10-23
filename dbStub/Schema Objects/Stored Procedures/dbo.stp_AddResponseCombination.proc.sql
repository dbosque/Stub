-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_AddResponseCombination]
	@response varchar(MAX),
	@responseDescription varchar(250),
	@Template_id int,
	@responsecombinatieDescription varchar(250),
	@response_id int output,
	@Combination_id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- insert response
	if @response_id = -1 
	begin
		insert response(Response,Description) values (@response,@responseDescription)
		set @response_id = SCOPE_IDENTITY()
	end

	declare @messagetype_id int
	select @messagetype_id = MessageTypeID from Template where TemplateID = @Template_id
	-- insert 
	insert Combination(MessageTypeID,TemplateID ,ResponseID,Description) 
	values( @messagetype_id, 
			@Template_id, 
			@response_id,
			@responsecombinatieDescription
			)
	set @Combination_id = SCOPE_IDENTITY()
END


