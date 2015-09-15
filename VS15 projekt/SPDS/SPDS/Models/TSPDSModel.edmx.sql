
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/15/2015 20:36:37
-- Generated from EDMX file: D:\TSPDS\Sem4.git\VS15 projekt\SPDS\SPDS\Models\TSPDSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [E15I4PRJ4Gr2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Institute] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PermissionPermissionId] int  NOT NULL
);
GO

-- Creating table 'PermissionSet'
CREATE TABLE [dbo].[PermissionSet] (
    [PermissionId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PermissionId] in table 'PermissionSet'
ALTER TABLE [dbo].[PermissionSet]
ADD CONSTRAINT [PK_PermissionSet]
    PRIMARY KEY CLUSTERED ([PermissionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PermissionPermissionId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_PermissionUser]
    FOREIGN KEY ([PermissionPermissionId])
    REFERENCES [dbo].[PermissionSet]
        ([PermissionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionUser'
CREATE INDEX [IX_FK_PermissionUser]
ON [dbo].[UserSet]
    ([PermissionPermissionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------