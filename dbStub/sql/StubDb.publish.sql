﻿/*
Deployment script for StubDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "StubDb"
:setvar DefaultFilePrefix "StubDb"
:setvar DefaultDataPath "D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC ON,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [Rol_Stub_Exec]...';


GO
CREATE ROLE [Rol_Stub_Exec]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [dbo].[Combination]...';


GO
CREATE TABLE [dbo].[Combination] (
    [CombinationID] INT           IDENTITY (1, 1) NOT NULL,
    [MessageTypeID] INT           NOT NULL,
    [TemplateID]    INT           NOT NULL,
    [ResponseID]    INT           NOT NULL,
    [Description]   VARCHAR (250) NULL,
    CONSTRAINT [PK_Combination] PRIMARY KEY CLUSTERED ([CombinationID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[CombinationXpath]...';


GO
CREATE TABLE [dbo].[CombinationXpath] (
    [CombinationXpathID] INT           IDENTITY (1, 1) NOT NULL,
    [XpathID]            INT           NOT NULL,
    [CombinationID]      INT           NOT NULL,
    [XpathValue]         VARCHAR (250) NULL,
    CONSTRAINT [PK_CombinationXpath] PRIMARY KEY CLUSTERED ([CombinationXpathID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[MessageType]...';


GO
CREATE TABLE [dbo].[MessageType] (
    [MessageTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Namespace]     VARCHAR (250) NOT NULL,
    [Rootnode]      VARCHAR (250) NOT NULL,
    [Description]   VARCHAR (250) NULL,
    CONSTRAINT [PK_MessageType] PRIMARY KEY CLUSTERED ([MessageTypeID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[MessageType].[IX_MessageType]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MessageType]
    ON [dbo].[MessageType]([Namespace] ASC, [Rootnode] ASC);


GO
PRINT N'Creating [dbo].[Request]...';


GO
CREATE TABLE [dbo].[Request] (
    [RequestID]   INT           IDENTITY (1, 1) NOT NULL,
    [Request]     VARCHAR (MAX) NOT NULL,
    [Description] VARCHAR (250) NULL,
    CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED ([RequestID] ASC) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestThumbprint]...';


GO
CREATE TABLE [dbo].[RequestThumbprint] (
    [RequestThumbPrintID] INT       IDENTITY (1, 1) NOT NULL,
    [ResponseID]          INT       NOT NULL,
    [RequestID]           INT       NOT NULL,
    [Thumbprint]          CHAR (40) NOT NULL,
    CONSTRAINT [PK_RequestThumbprint] PRIMARY KEY CLUSTERED ([RequestThumbPrintID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[RequestThumbprint].[IX_RequestThumbprint]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestThumbprint]
    ON [dbo].[RequestThumbprint]([Thumbprint] ASC);


GO
PRINT N'Creating [dbo].[Response]...';


GO
CREATE TABLE [dbo].[Response] (
    [ResponseID]  INT           IDENTITY (1, 1) NOT NULL,
    [Response]    VARCHAR (MAX) NOT NULL,
    [Description] VARCHAR (250) NULL,
    [StatusCode]  INT           NULL,
    [ContentType] VARCHAR (250) NULL,
    CONSTRAINT [PK_Response] PRIMARY KEY CLUSTERED ([ResponseID] ASC) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


GO
PRINT N'Creating [dbo].[StubLog]...';


GO
CREATE TABLE [dbo].[StubLog] (
    [StubLogID]           INT      IDENTITY (1, 1) NOT NULL,
    [RequestThumbprintID] INT      NULL,
    [CombinationID]       INT      NULL,
    [ResponseDatumTijd]   DATETIME NOT NULL,
    CONSTRAINT [PK_StubLog] PRIMARY KEY CLUSTERED ([StubLogID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Template]...';


GO
CREATE TABLE [dbo].[Template] (
    [TemplateID]    INT           IDENTITY (1, 1) NOT NULL,
    [MessageTypeID] INT           NOT NULL,
    [Description]   VARCHAR (250) NULL,
    CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[TemplateXpath]...';


GO
CREATE TABLE [dbo].[TemplateXpath] (
    [TemplateXpathID] INT IDENTITY (1, 1) NOT NULL,
    [TemplateID]      INT NOT NULL,
    [XpathID]         INT NOT NULL,
    CONSTRAINT [PK_TemplateXpath] PRIMARY KEY CLUSTERED ([TemplateXpathID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Xpath]...';


GO
CREATE TABLE [dbo].[Xpath] (
    [XpathID]     INT            IDENTITY (1, 1) NOT NULL,
    [Expression]  VARCHAR (1000) NOT NULL,
    [Description] VARCHAR (250)  NULL,
    CONSTRAINT [PK_Xpath] PRIMARY KEY CLUSTERED ([XpathID] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[FK_Template_TemplateID__Combination_TemplateID]...';


GO
ALTER TABLE [dbo].[Combination]
    ADD CONSTRAINT [FK_Template_TemplateID__Combination_TemplateID] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Template] ([TemplateID]);


GO
PRINT N'Creating [dbo].[FK_Response_ResponseID__Combination_ResponseID]...';


GO
ALTER TABLE [dbo].[Combination]
    ADD CONSTRAINT [FK_Response_ResponseID__Combination_ResponseID] FOREIGN KEY ([ResponseID]) REFERENCES [dbo].[Response] ([ResponseID]);


GO
PRINT N'Creating [dbo].[FK_MessageType_MessageTypeID__Combination_MessageTypeID]...';


GO
ALTER TABLE [dbo].[Combination]
    ADD CONSTRAINT [FK_MessageType_MessageTypeID__Combination_MessageTypeID] FOREIGN KEY ([MessageTypeID]) REFERENCES [dbo].[MessageType] ([MessageTypeID]);


GO
PRINT N'Creating [dbo].[FK_Combination_CombinationID__CombinationXpath_CombinationID]...';


GO
ALTER TABLE [dbo].[CombinationXpath]
    ADD CONSTRAINT [FK_Combination_CombinationID__CombinationXpath_CombinationID] FOREIGN KEY ([CombinationID]) REFERENCES [dbo].[Combination] ([CombinationID]);


GO
PRINT N'Creating [dbo].[FK_Xpath_XpathID__CombinationXpath_XpathID]...';


GO
ALTER TABLE [dbo].[CombinationXpath]
    ADD CONSTRAINT [FK_Xpath_XpathID__CombinationXpath_XpathID] FOREIGN KEY ([XpathID]) REFERENCES [dbo].[Xpath] ([XpathID]);


GO
PRINT N'Creating [dbo].[FK_Response_ResponseID__RequestThumbprint_ResponseID]...';


GO
ALTER TABLE [dbo].[RequestThumbprint]
    ADD CONSTRAINT [FK_Response_ResponseID__RequestThumbprint_ResponseID] FOREIGN KEY ([ResponseID]) REFERENCES [dbo].[Response] ([ResponseID]);


GO
PRINT N'Creating [dbo].[FK_Request_RequestID__RequestThumbprint_Request]...';


GO
ALTER TABLE [dbo].[RequestThumbprint]
    ADD CONSTRAINT [FK_Request_RequestID__RequestThumbprint_Request] FOREIGN KEY ([RequestID]) REFERENCES [dbo].[Request] ([RequestID]);


GO
PRINT N'Creating [dbo].[FK_MessageType_MessageTypeID__Template_MessageTypeID]...';


GO
ALTER TABLE [dbo].[Template]
    ADD CONSTRAINT [FK_MessageType_MessageTypeID__Template_MessageTypeID] FOREIGN KEY ([MessageTypeID]) REFERENCES [dbo].[MessageType] ([MessageTypeID]);


GO
PRINT N'Creating [dbo].[FK_Template_TemplateID__TemplateXpath_TemplateID]...';


GO
ALTER TABLE [dbo].[TemplateXpath]
    ADD CONSTRAINT [FK_Template_TemplateID__TemplateXpath_TemplateID] FOREIGN KEY ([TemplateID]) REFERENCES [dbo].[Template] ([TemplateID]);


GO
PRINT N'Creating [dbo].[FK_Xpath_XpathID__TemplateXpath_XpathID]...';


GO
ALTER TABLE [dbo].[TemplateXpath]
    ADD CONSTRAINT [FK_Xpath_XpathID__TemplateXpath_XpathID] FOREIGN KEY ([XpathID]) REFERENCES [dbo].[Xpath] ([XpathID]);


GO
PRINT N'Creating [dbo].[viewBerichtenLog]...';


GO
CREATE VIEW [dbo].[viewBerichtenLog]
AS
SELECT  dbo.StubLog.ResponseDatumTijd, 
		-- type bericht		
		berFindType = case isnull(dbo.StubLog.CombinationID,-1) when -1 
		then 'exact' else 'xpath' end,
		-- reponseID for this message received
		ResponseDescription = case isnull(dbo.StubLog.CombinationID,-1) when -1
		then 
		( 
		  select dbo.Response.Description from dbo.Response
		  inner join dbo.RequestThumbprint on dbo.RequestThumbprint.ResponseID = dbo.Response.ResponseID
		  -- select dbo.RequestThumbprint.ResponseID from dbo.RequestThumbprint
		  where dbo.RequestThumbprint.RequestThumbPrintID = dbo.StubLog.RequestThumbprintID 
		) 
		else 
		(
		  select dbo.Response.Description from dbo.Response
		  inner join dbo.Combination on dbo.Combination.ResponseID = dbo.Response.ResponseID
		  where dbo.Combination.CombinationID = dbo.StubLog.CombinationID 		
		) 
		end
		--,dbo.StubLog.StubLogId
FROM    dbo.StubLog
GO
PRINT N'Creating [dbo].[stp_AddCombinationXpathValue]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_AddCombinationXpathValue]
	@CombinationID int,
	@XpathID int,
	@XpathValue varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert CombinationXPath(	XpathID, CombinationID, XpathValue )
    values (@XpathID, @CombinationID, @XpathValue)
END
GO
PRINT N'Creating [dbo].[stp_AddRequestResponseByThumbprint]...';


GO
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
	   
	   INSERT Response(Response,Description) VALUES(@response, @responsedescription)
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
GO
PRINT N'Creating [dbo].[stp_AddResponseCombination]...';


GO
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
GO
PRINT N'Creating [dbo].[stp_MessageType_Insert]...';


GO
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
GO
PRINT N'Creating [dbo].[stp_RemoveCombinationWithResponse]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Verwijder Combination Met Bijbehorende Response
-- =============================================

CREATE PROCEDURE [dbo].[stp_RemoveCombinationWithResponse]
	@CombinationID int,
	@RespID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	SELECT ResponseID FROM [dbo].[Combination]
	WHERE [CombinationID] = @CombinationID AND ResponseID = @RespID
	
	DELETE FROM [dbo].[CombinationXpath]
	WHERE [CombinationID] = @CombinationID
	
	DELETE FROM [dbo].[Combination]
	WHERE [CombinationID] = @CombinationID
	
	DELETE FROM [dbo].[Response]
	WHERE [ResponseID] = @RespID
	
	SET NOCOUNT OFF;
END
GO
PRINT N'Creating [dbo].[stp_RemoveRequestResponseByThumbprint]...';


GO
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
GO
PRINT N'Creating [dbo].[stp_RemoveXpathByID]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Verwijder Xpath met ID
-- =============================================

CREATE PROCEDURE [dbo].[stp_RemoveXpathByID]
	@XpathID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	DELETE FROM [dbo].[TemplateXpath]
	WHERE [XpathID] = @XpathID
	
	DELETE FROM [dbo].[CombinationXpath]
	WHERE [XpathID] = @XpathID
	
	DELETE FROM [dbo].[Xpath]
	WHERE [XpathID] = @XpathID
	
	SET NOCOUNT OFF;
END
GO
PRINT N'Creating [dbo].[stp_SelectBerichtenLog]...';


GO
-- =============================================
-- Author:		.
-- Create date: 19 November 2007
-- Description:	Selecteer een gedeelte van de berichtenlog
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectBerichtenLog]
	@MagRegels as int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP(@MagRegels)* FROM viewBerichtenLog
	ORDER BY ResponseDatumTijd DESC
END
GO
PRINT N'Creating [dbo].[stp_SelectCombinationByMessageType]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectCombinationByMessageType]
	@Namespace as varchar(250), 
	@Rootnode as varchar(250) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --Ophalen Combinations aan de hand van Namespace en Rootnode 
	--SELECT @CombinationID = CombinationID, @ResponseID = ResponseID
	SELECT zc.CombinationID, zc.ResponseID, xp.Expression, zcxp.XpathValue
	FROM [dbo].[Combination] zc
		INNER JOIN [dbo].[CombinationXpath] zcxp on zc.CombinationID=zcxp.CombinationID
		INNER JOIN [dbo].[Xpath] xp on xp.XpathID=zcxp.XpathID
	WHERE EXISTS
		(SELECT MessageTypeID 
		FROM [dbo].[MessageType] 
		WHERE [Namespace] = @Namespace
			AND [Rootnode] = @Rootnode
			AND zc.MessageTypeID = MessageTypeID)
END
GO
PRINT N'Creating [dbo].[stp_selectmessagetypes]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_selectmessagetypes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from messagetype order by namespace
END
GO
PRINT N'Creating [dbo].[stp_SelectRequestResponseByLogID]...';


GO
CREATE PROCEDURE [dbo].[stp_SelectRequestResponseByLogID] 

	-- Add the parameters for the stored procedure here
	@StubLogID as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @ResponseID  as int
	Declare @RequestThumbprintID  as int
	Declare @CombinationID  as int


SELECT @RequestThumbprintID=RequestThumbprintID, @CombinationID=CombinationID 
	FROM [dbo].[StubLog]
	WHERE StubLogID = @StubLogID
IF @RequestThumbprintID is not null
		BEGIN
			--Ophalen bij de log behorende request en response
			SELECT [dbo].[Request].Request, [dbo].[Response].Response FROM [dbo].[RequestThumbprint]
			INNER JOIN [dbo].[Request] ON [dbo].[Request].RequestID = [dbo].[RequestThumbprint].RequestID
			INNER JOIN [dbo].[Response] ON [dbo].[Response].ResponseID = [dbo].[RequestThumbprint].ResponseID
			WHERE [dbo].[RequestThumbprint].RequestThumbprintID=@RequestThumbprintID
															     
		END
ELSE IF @CombinationID is not null
	BEGIN
			--Ophalen bij de log behorende response
						--Ophalen bij de log behorende request en response
			SELECT [dbo].[Response].Response FROM [dbo].[Combination]
			INNER JOIN [dbo].[Response] ON [dbo].[Response].ResponseID = [dbo].[Combination].ResponseID
			WHERE [dbo].[Combination].CombinationID=@CombinationID
	END
END
GO
PRINT N'Creating [dbo].[stp_SelectResponseByResponseID]...';


GO
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
	SELECT Response FROM [dbo].[Response]
	WHERE ResponseID=@ResponseID
			
	--Logrecord wegschrijven
	INSERT INTO [dbo].[StubLog]
			   ([CombinationID]           
			   ,[ResponseDatumTijd])
		 VALUES
			   (@CombinationID
			   ,getdate())
		
END
GO
PRINT N'Creating [dbo].[stp_SelectResponseByThumbprint]...';


GO
-- =============================================
-- Author:		.
-- Create date: 14-11-2007
-- Description:	Stored procedure voor het ophalen van een Responsebericht via een Thumbprint
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectResponseByThumbprint] 
	-- Add the parameters for the stored procedure here
	@Thumbprint as char(40)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @ResponseID  as int
	declare @RequestThumbprintID  as int

	--Zoeken naar Thumbprint
	SELECT @ResponseID=ResponseID, @RequestThumbprintID=RequestThumbprintID 
	FROM [dbo].[RequestThumbprint]
	WHERE Thumbprint = @Thumbprint

	IF @ResponseID is not null
		BEGIN
			--Ophalen gevonden Response
			SELECT Response FROM [dbo].[Response]
			WHERE ResponseID=@ResponseID
					
			--Logrecord wegschrijven
			INSERT INTO [dbo].[StubLog]
					   ([RequestThumbprintID]           
					   ,[ResponseDatumTijd])
				 VALUES
					   (@RequestThumbprintID
					   ,getdate())
		END
END
GO
PRINT N'Creating [dbo].[stp_selectResponses]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_selectResponses] @messagetypeid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select messagetype.Description, xpath.Description, XpathValue, * from Response
	join combination on combination.responseid = response.responseid
	join messagetype on messagetype.messagetypeid = combination.messagetypeid
	join combinationxpath on combinationxpath.combinationid = combination.combinationid
	join xpath on xpath.xpathid = combinationxpath.xpathid
	where combination.messagetypeid = @messagetypeid
END
GO
PRINT N'Creating [dbo].[stp_SelectTemplatesForMessageType]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectTemplatesForMessageType]
	@rootnode varchar(250),
	@namespace varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select distinct Template.Description,Template.TemplateID,MessageType.MessageTypeId from messagetype
	join Template on Template.MessageTypeId = MessageType.MessageTypeId
	join TemplateXPath on TemplateXPath.TemplateId = Template.TemplateId
	join XPath on TemplateXPath.XPathId= TemplateXPath.XpathId
	where MessageType.RootNode = @rootnode and MessageType.Namespace = @namespace
END
GO
PRINT N'Creating [dbo].[stp_SelectTemplateXpath]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Stored procedure voor het ophalen van een Responsebericht
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectTemplateXpath] 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT MessageTypeID, [Namespace], Rootnode, Description 
	FROM [dbo].[MessageType]

	SELECT TemplateID, MessageTypeID, Description 
	FROM [dbo].[Template]

	SELECT XpathID, Expression, Description 
	FROM [dbo].[Xpath]
	
	SELECT TemplateXpathID, TemplateID, XpathID 
	FROM [dbo].[TemplateXpath]
						
END
GO
PRINT N'Creating [dbo].[stp_SelectXpathListForTemplate]...';


GO
-- =============================================
-- Author:		.
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[stp_SelectXpathListForTemplate] 
	@Template_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	select distinct XPath.XPathId, XPath.Description, XPath.Expression  from XPath
	join TemplateXPath on TemplateXPath.XpathID = Xpath.XpathId
	join Template on Template.TemplateId = TemplateXPath.TemplateId	
	where Template.TemplateID = @Template_id
END
GO
PRINT N'Creating [dbo].[stp_SelectXpathsByCombinationID]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-01-2009
-- Description:	Selecteer Xpaths bij Combination
-- =============================================

CREATE PROCEDURE [dbo].[stp_SelectXpathsByCombinationID]
	@CombinationID int
	
AS
BEGIN	
	sET NOCOUNT ON;
	
	SELECT [XpathID] FROM [dbo].[CombinationXpath]
	WHERE [CombinationID] = @CombinationID
	
	SET NOCOUNT OFF;
END
GO
PRINT N'Creating [dbo].[stp_Template_Insert]...';


GO
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
GO
PRINT N'Creating [dbo].[stp_TemplateXpath_Delete]...';


GO
-- =============================================
-- Author:		.
-- Create date: 21-11-2007
-- Description:	Verwijder een TemplateXpath item
-- =============================================
CREATE PROCEDURE [dbo].[stp_TemplateXpath_Delete]	
	@TemplateID int,
	@XpathID int	
AS
BEGIN
	SET NOCOUNT OFF;
	DELETE FROM [dbo].[TemplateXpath]
	WHERE 
 	[TemplateID] = @TemplateID AND [XpathID] = @XpathID
	SET NOCOUNT ON;
END
GO
PRINT N'Creating [dbo].[stp_TemplateXpath_Insert]...';


GO
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
GO
PRINT N'Creating [dbo].[stp_Xpath_Insert]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Voeg een nieuwe Xpath item toe
-- =============================================
CREATE PROCEDURE [dbo].[stp_Xpath_Insert]	
	@Expression varchar(1000),
	@Description varchar(250)
	
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[XPath] (
		[Expression], [Description]
	) VALUES (
		@Expression, @Description
	);
	SET NOCOUNT ON;
	SELECT
		[XpathID],
		[Expression],
		[Description]
	FROM
		[dbo].[XPath]
	WHERE
		(XpathID = SCOPE_IDENTITY());

END
GO
PRINT N'Creating [dbo].[stp_Xpath_Update]...';


GO
-- =============================================
-- Author:		.
-- Create date: 20-11-2007
-- Description:	Update Xpath item
-- =============================================
CREATE PROCEDURE [dbo].[stp_Xpath_Update]
	@XpathID int,
	@Expression varchar(1000),
	@Description varchar(250)
	
AS
BEGIN	

	SET NOCOUNT OFF;
	UPDATE [dbo].[XPath] SET
		[Expression] = @Expression,
		[Description] = @Description
	WHERE XpathID = @XpathID
	;	
END
GO
PRINT N'Creating [dbo].[RequestThumbprint].[IX_RequestThumbprint].[MS_Description]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unieke thumbprint', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RequestThumbprint', @level2type = N'INDEX', @level2name = N'IX_RequestThumbprint';


GO
PRINT N'Creating [dbo].[viewBerichtenLog].[MS_DiagramPane1]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StubLog (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequestThumbprint (dbo)"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 114
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Request (dbo)"
            Begin Extent = 
               Top = 6
               Left = 484
               Bottom = 99
               Right = 635
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Response (dbo)"
            Begin Extent = 
               Top = 102
               Left = 484
               Bottom = 195
               Right = 635
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'viewBerichtenLog';


GO
PRINT N'Creating [dbo].[viewBerichtenLog].[MS_DiagramPane2]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'viewBerichtenLog';


GO
PRINT N'Creating [dbo].[viewBerichtenLog].[MS_DiagramPaneCount]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'viewBerichtenLog';


GO
PRINT N'Update complete.';


GO
