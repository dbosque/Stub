insert into configuration.Tenant 
values("__default", null, 1);

declare  @keyy  as int

set @keyy = 0


insert into configuration.Settings
values (@keyy,'Editor.SupportedContentTypes','application/soap+xml;application/json;application/xml;text/xml;text/plain')
insert into configuration.Settings
values (@keyy,'SQLServer.ConnectionString','Data Source=.;Initial Catalog=StubDb;Integrated Security=True')
insert into configuration.Settings
values (@keyy,'SQLServer.Provider','System.Data.SqlClient')
insert into configuration.Settings
values (@keyy,'Lite.ConnectionString','Data Source="dbstub.db"')
insert into configuration.Settings
values (@keyy,'Lite.Provider','System.Data.SQLite')
insert into configuration.Settings
values (@keyy,'MySQL.ConnectionString','Server=localhost;port=3306;Database=stubeditor;Uid=root;Pwd=root;')
insert into configuration.Settings
values (@keyy,'MySQL.Provider','System.Data.MySQL')

insert into configuration.Settings
values (0,'Log.System.Data.SqlClient','select l.StubLogID as id, l.Uri, l.ResponseDatumTijd, l.Request, r.ResponseText ,t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode
from StubLog l
	left join configuration.Tenant t on t.TenantId = l.TenantID
	left join dbo.Combination c on c.CombinationID = l.CombinationID
	left join dbo.MessageType m on m.MessageTypeID = l.MessageTypeId
	left join dbo.Template te on te.TemplateID = c.TemplateID
	left join dbo.Response r on r.ResponseID = c.ResponseID
	where l.StubLogID @compare @prevVal
order by ResponseDatumTijd desc')
insert into configuration.Settings
values (0,'Log.MySQL,'select l.StubLogID as id, l.Uri, l.ResponseDatumTijd, l.Request, t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode
from StubLog l
	left join Tenant t on t.TenantId = l.TenantID
	left join Combination c on c.CombinationID = l.CombinationID
	left join MessageType m on m.MessageTypeID = l.MessageTypeId
	left join Template te on te.TemplateID = c.TemplateID
	where l.StubLogID @compare @prevVal
order by ResponseDatumTijd desc
LIMIT @pagesize')
