create PROCEDURE [dbo].[stp_selectLog]
	@page as int, @pagesize as int 
AS
BEGIN
with Logs as
(
	select StubLogID, 
	ROW_NUMBER() OVER (ORDER BY ResponseDatumTijd desc) AS RowNumber  
	from dbo.StubLog 
)
  select l.ResponseDatumTijd, l.Request, t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode, RowNumber
  from Logs ll
    inner join dbo.StubLog l on l.StubLogID = ll.StubLogID
	inner join configuration.Tenant t on t.TenantId = l.TenantID
	inner join dbo.Combination c on c.CombinationID = l.CombinationID
	inner join dbo.MessageType m on m.MessageTypeID = c.MessageTypeID
	inner join dbo.Template te on te.TemplateID = c.TemplateID
  where ll.RowNumber > @page * @pagesize and ll.RowNumber <= (@page + 1)* @pagesize
END