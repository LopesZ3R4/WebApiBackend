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


CREATE TABLE Alerts (
    Id NVARCHAR(50) PRIMARY KEY,
    Type NVARCHAR(50),
    Occurrences NVARCHAR(50),
    EngineHoursType NVARCHAR(50),
    ValueAsDouble FLOAT,
    Unit NVARCHAR(50),
    MachineLinearTime INT,
    Bus NVARCHAR(50),
    Time DATETIME,
    LocationType NVARCHAR(50),
    Lat FLOAT,
    Lon FLOAT,
    Color NVARCHAR(50),
    Severity NVARCHAR(50),
    AcknowledgementStatus NVARCHAR(50),
    Ignored BIT,
    Invisible BIT
);

CREATE TABLE Durations (
    AlertId NVARCHAR(50) PRIMARY KEY,
    Type NVARCHAR(50),
    ValueAsInteger NVARCHAR(50),
    Unit NVARCHAR(50),
    FOREIGN KEY (AlertId) REFERENCES Alerts(Id)
);

CREATE TABLE Definitions (
    AlertId NVARCHAR(50) PRIMARY KEY,
    Id NVARCHAR(50),
    Type NVARCHAR(50),
    SuspectParameterName NVARCHAR(50),
    FailureModeIndicator NVARCHAR(50),
    Bus NVARCHAR(50),
    SourceAddress NVARCHAR(50),
    ThreeLetterAcronym NVARCHAR(50),
    Description NVARCHAR(MAX),
    FOREIGN KEY (AlertId) REFERENCES Alerts(Id)
);

CREATE TABLE Links (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AlertId NVARCHAR(50),
    DefinitionID NVARCHAR(50),
    Type NVARCHAR(50),
    Rel NVARCHAR(50),
    Uri NVARCHAR(MAX),
);


CREATE INDEX IDX_Durations_AlertId ON Durations (AlertId);
CREATE INDEX IDX_Definitions_AlertId ON Definitions (AlertId);
CREATE INDEX IDX_Links_AlertId ON Links (AlertId);

PRINT 'Initialization script completed successfully.'