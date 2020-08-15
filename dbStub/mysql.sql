 
CREATE TABLE Combination (
    `CombinationID` INT           AUTO_INCREMENT  NOT NULL,
    `MessageTypeID` INT           NOT NULL,
    `TemplateID`    INT           NOT NULL,
    `ResponseID`    INT           NOT NULL,
    `Description`   VARCHAR (250) NULL,
     PRIMARY KEY (`CombinationID`)
    )
CREATE TABLE CombinationXpath (
    `CombinationXpathID` INT           AUTO_INCREMENT  NOT NULL,
    `XpathID`            INT           NOT NULL,
    `CombinationID`      INT           NOT NULL,
    `XpathValue`         VARCHAR (250) NULL,
         PRIMARY KEY (`CombinationXpathID`)
    )
    
    CREATE TABLE MessageType (
    `MessageTypeID` INT           AUTO_INCREMENT  NOT NULL,
    `Namespace`     VARCHAR (250) NOT NULL,
    `Rootnode`      VARCHAR (250) NOT NULL,
    `Description`   VARCHAR (250) NULL,
     PRIMARY KEY (`MessageTypeID`)

)

CREATE TABLE Request (
    `RequestID`   INT           AUTO_INCREMENT  NOT NULL,
    `Request`     LONGTEXT  NOT NULL,
    `Description` VARCHAR (250) NULL,
  PRIMARY KEY (`RequestID`)
) 

CREATE TABLE RequestThumbprint (
    `RequestThumbPrintID` INT       AUTO_INCREMENT  NOT NULL,
    `ResponseID`          INT       NOT NULL,
    `RequestID`           INT       NOT NULL,
    `Thumbprint`          CHAR (40) NOT NULL,
     PRIMARY KEY (`RequestThumbPrintID`)
) 

CREATE TABLE Response (
    `ResponseID`  INT           AUTO_INCREMENT  NOT NULL,
    `ResponseText`    LONGTEXT  NOT NULL,
    `Description` VARCHAR (250) NULL,
    `StatusCode`  INT           NULL,
    `ContentType` VARCHAR (250) NULL,
     PRIMARY KEY (`ResponseID`)
)

CREATE TABLE StubLog (
    `StubLogID`           INT      AUTO_INCREMENT  NOT NULL,
   
    `CombinationID`       INT      NULL,
    `ResponseDatumTijd`   DATETIME(3) NOT NULL,
    `TenantID` INT NULL,
		`Request` LONGTEXT NOT NULL	,
		`Uri` LONGTEXT NULL, 
    `MessageTypeId` INT(11) NULL,
   PRIMARY KEY (`StubLogID`)
) 

CREATE TABLE Template (
    `TemplateID`    INT           AUTO_INCREMENT  NOT NULL,
    `MessageTypeID` INT           NOT NULL,
    `Description`   VARCHAR (250) NULL,
  PRIMARY KEY (`TemplateID`)

)

CREATE TABLE TemplateXpath (
    `TemplateXpathID` INT AUTO_INCREMENT  NOT NULL,
    `TemplateID`      INT NOT NULL,
    `XpathID`         INT NOT NULL,
  PRIMARY KEY (`TemplateXpathID`)
) 

CREATE TABLE Xpath (
    `XpathID`     INT            AUTO_INCREMENT  NOT NULL,
    `Expression`  VARCHAR (1000) NOT NULL,
    `Description` VARCHAR (250)  NULL,
   PRIMARY KEY (`XpathID`)
) 


CREATE TABLE `Settings`(
	`Id` int AUTO_INCREMENT NOT NULL,
	`TenantId` int NOT NULL,
	`Name` Longtext NOT NULL,
	`Value` Longtext NULL,
   PRIMARY KEY (`Id`)
)


CREATE TABLE `Tenant`(
	`TenantId` int AUTO_INCREMENT NOT NULL,
	`Name` Longtext NOT NULL,
	`Connectionstring` Longtext NULL,
	`Active` Tinyint NOT NULL,
 PRIMARY KEY (`TenantId`)
)


CREATE TABLE `TenantSecurity`(
	`TenantSecurityId` int AUTO_INCREMENT NOT NULL,
	`TenantId` int NOT NULL,
	`SecurityCode` Char(36) NOT NULL,
	`active` Tinyint NOT NULL,
 PRIMARY KEY (`TenantSecurityId`)
);



INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('2', '0', 'Log.MySQL', 'select l.StubLogID as Id, ifnull(r.ResponseText,r.StatusCode) as ResponseText,  l.Uri, l.ResponseDatumTijd, l.Request, t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode
from StubLog l
	left join Tenant t on t.TenantId = l.TenantID
	left join Combination c on c.CombinationID = l.CombinationID
	left join MessageType m on m.MessageTypeID = c.MessageTypeId
	left join Template te on te.TemplateID = c.TemplateID
left join Response r on r.ResponseID = c.ResponseID
	where l.StubLogID @compare @prevVal
order by ResponseDatumTijd desc
LIMIT @pagesize');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('5', '0', 'SQLServer.ConnectionString', 'Data Source=.;Initial Catalog=StubDb;Integrated Security=True');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('7', '0', 'SQLServer.Provider', 'SQLServer');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('8', '0', 'Lite.ConnectionString', 'Data Source=''.dbstub.db''');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('9', '0', 'Lite.Provider', 'SQLite');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('10', '0', 'Log.SQLServer', 'select l.StubLogID as id, l.Uri, l.ResponseDatumTijd, l.Request,ISNULL(r.ResponseText,r.StatusCode) ,t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode
from StubLog l
	left join configuration.Tenant t on t.TenantId = l.TenantID
	left join dbo.Combination c on c.CombinationID = l.CombinationID
	left join dbo.MessageType m on m.MessageTypeID = l.MessageTypeId
	left join dbo.Template te on te.TemplateID = c.TemplateID
	left join dbo.Response r on r.ResponseID = c.ResponseID
	where l.StubLogID @compare @prevVal
order by ResponseDatumTijd desc');
INSERT INTO "settings" (`Id`, `TenantId`, `Name`, `Value`) VALUES ('11', '0', 'Log.SQLite', 'select l.StubLogID as Id, ifnull(r.ResponseText,r.StatusCode) as ResponseText,  l.Uri, l.ResponseDatumTijd, l.Request, t.Name as Tenant, te.Description as Template, c.Description as Combination, m.Namespace, m.Rootnode
from StubLog l
	left join Tenant t on t.TenantId = l.TenantID
	left join Combination c on c.CombinationID = l.CombinationID
	left join MessageType m on m.MessageTypeID = l.MessageTypeId
	left join Template te on te.TemplateID = c.TemplateID
left join Response r on r.ResponseID = c.ResponseID
	where l.StubLogID @compare @prevVal
order by ResponseDatumTijd desc
LIMIT @pagesize');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('12', '0', 'MySQL.Provider', 'MySQL');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('13', '0', 'MySQL.ConnectionString', 'Server=localhost;port=3306;Database=stubeditor;Uid=root;Pwd=root;');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('14', '0', 'Hosting.WebApiBaseUrl', 'http://localhost:8081');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('15', '0', 'Hosting.ConfigurationUrl', 'http://localhost:8082');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('16', '0', 'Hosting.SocketPort', '8084');
INSERT INTO `settings` (`Id`, `TenantId`, `Name`, `Value`) VALUES ('17', '0', 'Hosting.WCFBaseUrl', 'http://localhost:8083');