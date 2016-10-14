
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/05/2010 16:17:53
-- Generated from EDMX file: M:\Book Work\Code\Chapter14\Recipe7\Recipe7\Recipe7.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EFRecipes];
GO
IF SCHEMA_ID(N'Chapter14') IS NULL EXECUTE(N'CREATE SCHEMA [Chapter14]');
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

-- Creating table 'PhonePlans'
CREATE TABLE [Chapter14].[PhonePlans] (
[PhonePlanId] int  IDENTITY(1,1) NOT NULL,
[Minutes] int   NOT NULL,
[Cost] decimal(18,0)   NOT NULL,
[TimeStamp] TIMESTAMP   NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PhonePlanId] in table 'PhonePlans'
ALTER TABLE [Chapter14].[PhonePlans]
ADD CONSTRAINT [PK_PhonePlans]
    PRIMARY KEY CLUSTERED ([PhonePlanId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------