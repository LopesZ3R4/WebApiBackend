EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'xp_cmdshell', 1;
GO
RECONFIGURE;
GO

PRINT 'Creating database...'
CREATE DATABASE bd;
GO

PRINT 'Setting database as current...'

USE bd;
GO

PRINT 'Creating admin user...'

CREATE LOGIN adm WITH PASSWORD = '@Admin1234';
GO

PRINT 'Granting sysadmin role to admin user...'

ALTER SERVER ROLE sysadmin ADD MEMBER adm;
GO

PRINT 'Creating Users table...'

CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(128) NOT NULL,
    UserType NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Token NVARCHAR(255)
);
CREATE INDEX idx_username ON Users (Username);
CREATE INDEX idx_email ON Users (Email);

INSERT INTO Users (Username,Password,UserType,Email) values ('admin','34b9b7e38c513dd5b4001aa7b2f05f15444c7c520d5b851b28ef22e462811cc9','Admin','admin@sou.br');
go

CREATE TABLE Alerts (
    Id Int PRIMARY KEY,
    Type NVARCHAR(50),
    DurationType NVARCHAR(50),
    DurationValue FLOAT,
    DurationUnit NVARCHAR(50),
    Occurrences NVARCHAR(50),
    EngineHoursType NVARCHAR(50),
    EngineHoursValue FLOAT,
    EngineHoursUnit NVARCHAR(50),
    MachineLinearTime INT,
    Bus Int,
    DefinitionId Int,
    Time DATETIME,
    LocationType NVARCHAR(50),
    Lat FLOAT,
    Lon FLOAT,
    Color NVARCHAR(50),
    Severity NVARCHAR(50),
    AcknowledgementStatus NVARCHAR(50),
    Ignored BIT,
    Invisible BIT,
    LinkType NVARCHAR(50),
    LinkRel NVARCHAR(50),
    LinkUri NVARCHAR(255),
    DefinitionLinkType NVARCHAR(50),
    DefinitionLinkRel NVARCHAR(50),
    DefinitionLinkUri NVARCHAR(255),
    DefinitionId Int,
    DefinitionType NVARCHAR(50),
    DefinitionSuspectParameterName NVARCHAR(50),
    DefinitionFailureModeIndicator NVARCHAR(50),
    DefinitionBus Int,
    DefinitionSourceAddress NVARCHAR(50),
    DefinitionThreeLetterAcronym NVARCHAR(50),
    DefinitionDescription NVARCHAR(MAX),
);

CREATE INDEX IDX_AlertsID ON Alerts (Id);
CREATE INDEX IDX_Alerts_Type ON Alerts (Type);
CREATE INDEX IDX_Alerts_Color ON Alerts (Color);
CREATE INDEX IDX_Alerts_Severity ON Alerts (Severity);
CREATE INDEX IDX_Alerts_Time ON Alerts (Time);

PRINT 'Initialization script completed successfully.'