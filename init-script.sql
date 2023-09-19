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


CREATE TABLE Alert (
    alert_id INT PRIMARY KEY,
    total INT,
    color NVARCHAR(255),
    severity NVARCHAR(255),
    acknowledgementStatus NVARCHAR(255),
    ignored BIT,
    invisible BIT,
    time DATETIME
);

CREATE TABLE Link (
    link_id INT PRIMARY KEY,
    rel NVARCHAR(255),
    uri NVARCHAR(255),
    alert_id INT FOREIGN KEY REFERENCES Alert(alert_id)
);

CREATE TABLE Value (
    value_id INT PRIMARY KEY,
    alert_id INT FOREIGN KEY REFERENCES Alert(alert_id),
    type NVARCHAR(255),
    occurrences NVARCHAR(255),
    machineLinearTime BIGINT,
    bus NVARCHAR(255),
    id NVARCHAR(255)
);

CREATE TABLE Duration (
    duration_id INT PRIMARY KEY,
    value_id INT FOREIGN KEY REFERENCES Value(value_id),
    type NVARCHAR(255),
    valueAsInteger NVARCHAR(255),
    unit NVARCHAR(255)
);

CREATE TABLE EngineHours (
    engine_hours_id INT PRIMARY KEY,
    value_id INT FOREIGN KEY REFERENCES Value(value_id),
    type NVARCHAR(255),
    valueAsDouble NVARCHAR(255),
    unit NVARCHAR(255)
);

CREATE TABLE Location (
    location_id INT PRIMARY KEY,
    value_id INT FOREIGN KEY REFERENCES Value(value_id),
    type NVARCHAR(255),
    lat DECIMAL(9,6),
    lon DECIMAL(9,6)
);

CREATE TABLE Definition (
    definition_id INT PRIMARY KEY,
    value_id INT FOREIGN KEY REFERENCES Value(value_id),
    type NVARCHAR(255),
    suspectParameterName NVARCHAR(255),
    failureModeIndicator NVARCHAR(255),
    bus NVARCHAR(255),
    sourceAddress NVARCHAR(255),
    threeLetterAcronym NVARCHAR(255),
    id NVARCHAR(255),
    description NVARCHAR(MAX)
);

CREATE TABLE DefinitionLink (
    definition_link_id INT PRIMARY KEY,
    definition_id INT FOREIGN KEY REFERENCES Definition(definition_id),
    type NVARCHAR(255),
    rel NVARCHAR(255),
    uri NVARCHAR(255)
);

CREATE TABLE AlertLink (
    alert_link_id INT PRIMARY KEY,
    value_id INT FOREIGN KEY REFERENCES Value(value_id),
    type NVARCHAR(255),
    rel NVARCHAR(255),
    uri NVARCHAR(255)
);

CREATE INDEX idx_alert ON Link(alert_id);
CREATE INDEX idx_value ON Duration(value_id);
CREATE INDEX idx_value ON EngineHours(value_id);
CREATE INDEX idx_value ON Location(value_id);
CREATE INDEX idx_value ON Definition(value_id);
CREATE INDEX idx_definition ON DefinitionLink(definition_id);
CREATE INDEX idx_value ON AlertLink(value_id);

PRINT 'Initialization script completed successfully.'