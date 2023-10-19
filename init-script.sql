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

CREATE TABLE Clients
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Address NVARCHAR(MAX),
    ContactNumber NVARCHAR(50),
    Email NVARCHAR(255) UNIQUE
);

CREATE INDEX IDX_Clients ON Clients (Id);

CREATE TABLE Machines (
    Id INT PRIMARY KEY,
    ClientId INT,
    VisualizationCategory NVARCHAR(MAX),
    MachineCategories NVARCHAR(MAX),
    Category NVARCHAR(MAX),
    Make NVARCHAR(MAX),
    Model NVARCHAR(MAX),
    DetailMachineCode NVARCHAR(MAX),
    [Type] NVARCHAR(MAX),
    ProductKey INT,
    EngineSerialNumber NVARCHAR(MAX),
    TelematicsState NVARCHAR(MAX),
    Capabilities NVARCHAR(MAX),
    Terminals NVARCHAR(MAX),
    Displays NVARCHAR(MAX),
    [Guid] NVARCHAR(MAX),
    ModelYear INT,
    Vin NVARCHAR(MAX),
    ExternalId INT,
    Name NVARCHAR(MAX),
    Display NVARCHAR(MAX),
    FOREIGN KEY (ClientId) REFERENCES Clients (Id)
);

CREATE INDEX IDX_Machines ON Machines (Id);
CREATE INDEX IDX_Machines_ClienteID ON Machines (ClientId);

CREATE TABLE Alerts (
    Id Int PRIMARY KEY,
    MachineId int,
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
    Sent bit DEFAULT 0,
    FOREIGN KEY (MachineId) REFERENCES Machines (Id)
);

CREATE INDEX IDX_AlertsID ON Alerts (Id);
CREATE INDEX IDX_Alerts_Type ON Alerts (Type);
CREATE INDEX IDX_Alerts_Color ON Alerts (Color);
CREATE INDEX IDX_Alerts_Severity ON Alerts (Severity);
CREATE INDEX IDX_Alerts_Time ON Alerts (Time);

CREATE TABLE Encaminhamento
(
    IdEncaminhamento INT IDENTITY(1,1) PRIMARY KEY,
    AlertId INT NOT NULL,
    IdUsuario NVARCHAR(255),
    Motivo NVARCHAR(MAX),
    IdEmpresa INT NOT NULL,
    EncaminhamentoAtivo BIT,
    DataInclusao DATETIME,
    DataAlteracao DATETIME,
    UsuarioInc NVARCHAR(255),
    UsuarioAlt NVARCHAR(255),
    OrigemRetorno INT,
    FOREIGN KEY (AlertId) REFERENCES Alerts(Id),
    FOREIGN KEY (IdEmpresa) REFERENCES Clients(Id)
);

CREATE INDEX IDX_Encaminhamento ON Encaminhamento (IdEncaminhamento);
CREATE INDEX IDX_Encaminhamento_AlertID ON Encaminhamento (AlertId);

INSERT INTO Clients (Name, Address, ContactNumber, Email)
VALUES ('Rota Oeste', '123 Main St, Anytown, USA', '123-456-7890', 'contact@rotaoeste.com');

INSERT INTO Machines 
(
    Id, 
    ClientId, 
    VisualizationCategory, 
    MachineCategories, 
    Category, 
    Make, 
    Model, 
    DetailMachineCode, 
    [Type], 
    ProductKey, 
    EngineSerialNumber, 
    TelematicsState, 
    Capabilities, 
    Terminals, 
    Displays, 
    [Guid], 
    ModelYear, 
    Vin, 
    ExternalId, 
    Name, 
    Display
)
VALUES 
(
    1, -- Id
    1, -- ClientId
    'Excavator', -- VisualizationCategory
    'Construction', -- MachineCategories
    'Heavy', -- Category
    'John Deere', -- Make
    'E360', -- Model
    'JD360', -- DetailMachineCode
    'Excavator', -- Type
    123456, -- ProductKey
    'ESN123456', -- EngineSerialNumber
    'Active', -- TelematicsState
    'GPS, Engine Monitoring', -- Capabilities
    'Terminal 1', -- Terminals
    'Display 1', -- Displays
    'GUID123456', -- Guid
    2020, -- ModelYear
    'VIN123456', -- Vin
    1, -- ExternalId
    'John Deere E360', -- Name
    'Display 1' -- Display
);

-- Inserindo o primeiro conjunto de dados
INSERT INTO Alerts (Id, MachineId, Type, DurationType, DurationValue, DurationUnit, Occurrences, EngineHoursType, EngineHoursValue, EngineHoursUnit, MachineLinearTime, Bus, Time, LocationType, Lat, Lon, Color, Severity, AcknowledgementStatus, Ignored, Invisible, LinkType, LinkRel, LinkUri, DefinitionLinkType, DefinitionLinkRel, DefinitionLinkUri, DefinitionId, DefinitionType, DefinitionSuspectParameterName, DefinitionFailureModeIndicator, DefinitionBus, DefinitionSourceAddress, DefinitionThreeLetterAcronym, DefinitionDescription)
VALUES (
    '123456789',
    1,
    'DiagnosticTroubleCodeAlert', 
    'measurementAsInteger', 
    '3', 
    'Seconds', 
    '1', 
    'EngineHours', 
    '668.25', 
    'Hours', 
    '36951157', 
    '0', 
    '2020-05-13T19:15:11.000Z', 
    'Point', 
    '41.123456', 
    '-90.234567', 
    'BLUE', 
    'INFO', 
    'N', 
    0, 
    0, 
    'Link', 
    'machine', 
    'https://sandboxapi.deere.com/platform/machines/123456', 
    'Link', 
    'self', 
    'https://sandboxapi.deere.com/platform/api/alertTypes/diagnosticTroubleCodeAlert/definitions/1328105', 
    '1234567', 
    'DiagnosticTroubleCodeAlertDefinition', 
    '524019', 
    '31', 
    '0', 
    '140', 
    'AIC', 
    'Other AIC 524019.31 Reverser lever left in incorrect position. - Return to park to attempt recovery.'
);

-- Inserindo o segundo conjunto de dados
INSERT INTO Alerts (Id, MachineId, Type, DurationType, DurationValue, DurationUnit, Occurrences, EngineHoursType, EngineHoursValue, EngineHoursUnit, MachineLinearTime, Bus, Time, LocationType, Lat, Lon, Color, Severity, AcknowledgementStatus, Ignored, Invisible, LinkType, LinkRel, LinkUri, DefinitionLinkType, DefinitionLinkRel, DefinitionLinkUri, DefinitionId, DefinitionType, DefinitionSuspectParameterName, DefinitionFailureModeIndicator, DefinitionBus, DefinitionSourceAddress, DefinitionThreeLetterAcronym, DefinitionDescription)
VALUES (
    '123456790',
    1,
    'DiagnosticTroubleCodeAlert', 
    'measurementAsInteger', 
    '5', 
    'Seconds', 
    '2', 
    'EngineHours', 
    '670.50', 
    'Hours', 
    '36951159', 
    '1', 
    '2020-05-13T19:16:11.000Z', 
    'Point', 
    '41.123457', 
    '-90.234568', 
    'RED', 
    'WARNING', 
    'N', 
    0, 
    0, 
    'Link', 
    'machine', 
    'https://sandboxapi.deere.com/platform/machines/123457', 
    'Link', 
    'self', 
    'https://sandboxapi.deere.com/platform/api/alertTypes/diagnosticTroubleCodeAlert/definitions/1328106', 
    '1234568', 
    'DiagnosticTroubleCodeAlertDefinition', 
    '524020', 
    '32', 
    '1', 
    '141', 
    'AID', 
    'Other AID 524020.32 Reverser lever right in incorrect position. - Return to park to attempt recovery.'
);

-- Inserindo o terceiro conjunto de dados
INSERT INTO Alerts (Id, MachineId, Type, DurationType, DurationValue, DurationUnit, Occurrences, EngineHoursType, EngineHoursValue, EngineHoursUnit, MachineLinearTime, Bus, Time, LocationType, Lat, Lon, Color, Severity, AcknowledgementStatus, Ignored, Invisible, LinkType, LinkRel, LinkUri, DefinitionLinkType, DefinitionLinkRel, DefinitionLinkUri, DefinitionId, DefinitionType, DefinitionSuspectParameterName, DefinitionFailureModeIndicator, DefinitionBus, DefinitionSourceAddress, DefinitionThreeLetterAcronym, DefinitionDescription)
VALUES (
    '123456791',
    1,
    'DiagnosticTroubleCodeAlert', 
    'measurementAsInteger', 
    '7', 
    'Seconds', 
    '3', 
    'EngineHours', 
    '672.75', 
    'Hours', 
    '36951161', 
    '2', 
    '2020-05-13T19:17:11.000Z', 
    'Point', 
    '41.123458', 
    '-90.234569', 
    'YELLOW', 
    'CRITICAL', 
    'N', 
    0, 
    0, 
    'Link', 
    'machine', 
    'https://sandboxapi.deere.com/platform/machines/123458', 
    'Link', 
    'self', 
    'https://sandboxapi.deere.com/platform/api/alertTypes/diagnosticTroubleCodeAlert/definitions/1328107', 
    '1234569', 
    'DiagnosticTroubleCodeAlertDefinition', 
    '524021', 
    '33', 
    '2', 
    '142', 
    'AIE', 
    'Other AIE 524021.33 Reverser lever up in incorrect position. - Return to park to attempt recovery.'
);

-- Inserindo um encaminhamento para o primeiro alerta com o usuário "admin"
INSERT INTO Encaminhamento (AlertId, IdUsuario, Motivo, IdEmpresa, EncaminhamentoAtivo, DataInclusao, DataAlteracao, UsuarioInc, UsuarioAlt, OrigemRetorno)
VALUES (
    123456789, -- Substitua pelo ID do primeiro alerta inserido
    'admin',
    'Alerta Vermleho',
    1, -- Substitua pelo ID da empresa
    1, -- EncaminhamentoAtivo (1 para ativo, 0 para inativo)
    GETDATE(), -- Data de inclusão (data atual)
    GETDATE(), -- Data de alteração (data atual)
    'admin', -- Usuário de inclusão
    'admin', -- Usuário de alteração
    1 -- Origem do retorno (substitua pelo valor apropriado)
);

PRINT 'Initialization script completed successfully.'