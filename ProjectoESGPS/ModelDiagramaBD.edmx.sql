
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2016 17:51:07
-- Generated from EDMX file: C:\Users\HugoDuarte\documents\visual studio 2015\Projects\ProjectoESGPS\ProjectoESGPS\ModelDiagramaBD.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PatientAppointement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AppointementSet] DROP CONSTRAINT [FK_PatientAppointement];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientClinicalData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CDataSet] DROP CONSTRAINT [FK_PatientClinicalData];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientFiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FilesSet] DROP CONSTRAINT [FK_PatientFiles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[AppointementSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppointementSet];
GO
IF OBJECT_ID(N'[dbo].[PatientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientSet];
GO
IF OBJECT_ID(N'[dbo].[CDataSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CDataSet];
GO
IF OBJECT_ID(N'[dbo].[FilesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pw] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Fname] nvarchar(max)  NOT NULL,
    [Lname] nvarchar(max)  NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AppointementSet'
CREATE TABLE [dbo].[AppointementSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Diagnosis] nvarchar(max)  NULL,
    [Medication] nvarchar(max)  NULL,
    [Obs] nvarchar(max)  NULL,
    [Doctor] nvarchar(max)  NOT NULL,
    [Patient_Id] int  NOT NULL
);
GO

-- Creating table 'PatientSet'
CREATE TABLE [dbo].[PatientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fname] nvarchar(max)  NOT NULL,
    [Lname] nvarchar(max)  NOT NULL,
    [SNS] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [CData] nvarchar(max)  NULL,
    [DateBirth] datetime  NOT NULL
);
GO

-- Creating table 'CDataSet'
CREATE TABLE [dbo].[CDataSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Altura] nvarchar(max)  NOT NULL,
    [Peso] nvarchar(max)  NOT NULL,
    [Alergias] nvarchar(max)  NULL,
    [SPO2] nvarchar(max)  NULL,
    [HR] nvarchar(max)  NULL,
    [PS] nvarchar(max)  NULL,
    [Temp] nvarchar(max)  NULL,
    [Patient_Id] int  NOT NULL
);
GO

-- Creating table 'FilesSet'
CREATE TABLE [dbo].[FilesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Dir] nvarchar(max)  NOT NULL,
    [Patient_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AppointementSet'
ALTER TABLE [dbo].[AppointementSet]
ADD CONSTRAINT [PK_AppointementSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientSet'
ALTER TABLE [dbo].[PatientSet]
ADD CONSTRAINT [PK_PatientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CDataSet'
ALTER TABLE [dbo].[CDataSet]
ADD CONSTRAINT [PK_CDataSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FilesSet'
ALTER TABLE [dbo].[FilesSet]
ADD CONSTRAINT [PK_FilesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Patient_Id] in table 'AppointementSet'
ALTER TABLE [dbo].[AppointementSet]
ADD CONSTRAINT [FK_PatientAppointement]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PatientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientAppointement'
CREATE INDEX [IX_FK_PatientAppointement]
ON [dbo].[AppointementSet]
    ([Patient_Id]);
GO

-- Creating foreign key on [Patient_Id] in table 'CDataSet'
ALTER TABLE [dbo].[CDataSet]
ADD CONSTRAINT [FK_PatientClinicalData]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PatientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientClinicalData'
CREATE INDEX [IX_FK_PatientClinicalData]
ON [dbo].[CDataSet]
    ([Patient_Id]);
GO

-- Creating foreign key on [Patient_Id] in table 'FilesSet'
ALTER TABLE [dbo].[FilesSet]
ADD CONSTRAINT [FK_PatientFiles]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PatientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientFiles'
CREATE INDEX [IX_FK_PatientFiles]
ON [dbo].[FilesSet]
    ([Patient_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------