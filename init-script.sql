-- Enable xp_cmdshell
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'xp_cmdshell', 1;
GO
RECONFIGURE;
GO
-- Create database
PRINT 'Creating database...'
CREATE DATABASE bd;
GO

PRINT 'Setting database as current...'
-- Set database as current
USE bd;
GO

PRINT 'Creating admin user...'
-- Create admin user
CREATE LOGIN adm WITH PASSWORD = '@Admin1234';
GO

PRINT 'Granting sysadmin role to admin user...'
-- Grant sysadmin role to admin user
ALTER SERVER ROLE sysadmin ADD MEMBER adm;
GO

PRINT 'Creating Users table...'
-- Create Users table
CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    HashedPassword NVARCHAR(128) NOT NULL,
    UserType NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Token NVARCHAR(255)
);
CREATE INDEX idx_username ON Users (Username);
CREATE INDEX idx_email ON Users (Email);


-- File Path: ./init-script.sql
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
    DefinitionLinkUri NVARCHAR(255) 
);

CREATE TABLE Definitions (
    AlertId Int PRIMARY KEY,
    Id Int,
    Type NVARCHAR(50),
    SuspectParameterName NVARCHAR(50),
    FailureModeIndicator NVARCHAR(50),
    Bus Int,
    SourceAddress NVARCHAR(50),
    ThreeLetterAcronym NVARCHAR(50),
    Description NVARCHAR(MAX),
    FOREIGN KEY (AlertId) REFERENCES Alerts(Id)
);

CREATE INDEX IDX_Definitions_AlertId ON Definitions (AlertId);

PRINT 'Initialization script completed successfully.'