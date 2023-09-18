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
    Email NVARCHAR(255) NOT NULL UNIQUE
);
CREATE INDEX idx_username ON Users (Username);
CREATE INDEX idx_email ON Users (Email);

PRINT 'Initialization script completed successfully.'