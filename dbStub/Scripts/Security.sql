
CREATE LOGIN stubreader 
	WITH PASSWORD = 'dbosque!23' 
GO

CREATE USER stubreader
	FOR LOGIN stubreader
	WITH DEFAULT_SCHEMA = dbo
GO

-- Add user to the database owner role
EXEC sp_addrolemember N'db_datareader', N'stubreader'
GO

grant select,insert,update on schema ::configuration TO stubreader

GRANT INSERT ON 
   dbo.StubLog
TO stubreader