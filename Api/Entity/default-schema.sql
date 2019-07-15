
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/21/2018 14:00:29
-- Generated from EDMX file: D:\Thirumalai\TFS\Tools\Retrospective.Application\Retrospective.Application\Retrospective.Application.API\Entity\RetroToolModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RetroTool];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbl_mst_agiledescinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mst_agiledescinfo];
GO
IF OBJECT_ID(N'[dbo].[tbl_mst_imageinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mst_imageinfo];
GO
IF OBJECT_ID(N'[dbo].[tbl_mst_imageinfodetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mst_imageinfodetails];
GO
IF OBJECT_ID(N'[dbo].[tbl_mst_projectinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mst_projectinfo];
GO
IF OBJECT_ID(N'[dbo].[tbl_mst_retroinfodetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_mst_retroinfodetails];
GO
IF OBJECT_ID(N'[dbo].[tbl_trn_retroinfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_trn_retroinfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_mst_agiledescinfo'
CREATE TABLE [dbo].[tbl_mst_agiledescinfo] (
    [agiledescinfo_id] int IDENTITY(1,1) NOT NULL,
    [agiledescinfo_text] nvarchar(max)  NOT NULL,
    [agiledescinfo_type] nvarchar(50)  NULL,
    [agiledescinfo_remarks] nvarchar(max)  NULL,
    [agiledescinfo_isdeleted] bit  NOT NULL
);
GO

-- Creating table 'tbl_mst_imageinfo'
CREATE TABLE [dbo].[tbl_mst_imageinfo] (
    [imageinfo_id] int IDENTITY(1,1) NOT NULL,
    [imageinfo_name] nvarchar(50)  NOT NULL,
    [imageinfo_path] nvarchar(100)  NOT NULL,
    [imageinfo_isdeleted] bit  NULL
);
GO

-- Creating table 'tbl_mst_projectinfo'
CREATE TABLE [dbo].[tbl_mst_projectinfo] (
    [projectinfo_id] int IDENTITY(1,1) NOT NULL,
    [projectinfo_name] nvarchar(50)  NOT NULL,
    [projectinfo_portfolio] nvarchar(50)  NULL,
    [projectinfo_sow_name] nvarchar(50)  NULL,
    [projectinfo_sow_coverage] nvarchar(50)  NULL,
    [projectinfo_remarks] nvarchar(max)  NULL,
    [projectinfo_isdeleted] bit  NOT NULL
);
GO

-- Creating table 'tbl_trn_retroinfo'
CREATE TABLE [dbo].[tbl_trn_retroinfo] (
    [retroinfo_id] int IDENTITY(1,1) NOT NULL,
    [retroinfo_name] nvarchar(50)  NOT NULL,
    [retroinfo_projectinfo_id] int  NOT NULL,
    [retroinfo_sprint] nvarchar(max)  NOT NULL,
    [retroinfo_date] nvarchar(50)  NOT NULL,
    [retroinfo_imagepath] nvarchar(max)  NOT NULL,
    [retroinfo_imageinfo_id] int  NOT NULL,
    [retroinfo_isdeleted] bit  NULL
);
GO

-- Creating table 'tbl_mst_imageinfodetails'
CREATE TABLE [dbo].[tbl_mst_imageinfodetails] (
    [imageinfodetails_id] int IDENTITY(1,1) NOT NULL,
    [imageinfodetails_text] nvarchar(50)  NOT NULL,
    [imageinfodetails_type] nvarchar(50)  NOT NULL,
    [imageinfodetails_imageinfo_id] int  NOT NULL,
    [imageinfodetails_color] nvarchar(50)  NULL,
    [imageinfodetails_isdeleted] bit  NULL
);
GO

-- Creating table 'tbl_mst_retroinfodetails'
CREATE TABLE [dbo].[tbl_mst_retroinfodetails] (
    [retroinfodetails_id] int IDENTITY(1,1) NOT NULL,
    [retroinfodetails_retroinfo_id] int  NOT NULL,
    [retroinfodetails_text] nvarchar(max)  NOT NULL,
    [retroinfodetails_top] nvarchar(50)  NOT NULL,
    [retroinfodetails_left] nvarchar(50)  NOT NULL,
    [retroinfodetails_isdeleted] bit  NOT NULL,
    [retroinfodetails_category_id] int  NULL,
    [retroinfodetails_color] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [agiledescinfo_id] in table 'tbl_mst_agiledescinfo'
ALTER TABLE [dbo].[tbl_mst_agiledescinfo]
ADD CONSTRAINT [PK_tbl_mst_agiledescinfo]
    PRIMARY KEY CLUSTERED ([agiledescinfo_id] ASC);
GO

-- Creating primary key on [imageinfo_id] in table 'tbl_mst_imageinfo'
ALTER TABLE [dbo].[tbl_mst_imageinfo]
ADD CONSTRAINT [PK_tbl_mst_imageinfo]
    PRIMARY KEY CLUSTERED ([imageinfo_id] ASC);
GO

-- Creating primary key on [projectinfo_id] in table 'tbl_mst_projectinfo'
ALTER TABLE [dbo].[tbl_mst_projectinfo]
ADD CONSTRAINT [PK_tbl_mst_projectinfo]
    PRIMARY KEY CLUSTERED ([projectinfo_id] ASC);
GO

-- Creating primary key on [retroinfo_id] in table 'tbl_trn_retroinfo'
ALTER TABLE [dbo].[tbl_trn_retroinfo]
ADD CONSTRAINT [PK_tbl_trn_retroinfo]
    PRIMARY KEY CLUSTERED ([retroinfo_id] ASC);
GO

-- Creating primary key on [imageinfodetails_id] in table 'tbl_mst_imageinfodetails'
ALTER TABLE [dbo].[tbl_mst_imageinfodetails]
ADD CONSTRAINT [PK_tbl_mst_imageinfodetails]
    PRIMARY KEY CLUSTERED ([imageinfodetails_id] ASC);
GO

-- Creating primary key on [retroinfodetails_id] in table 'tbl_mst_retroinfodetails'
ALTER TABLE [dbo].[tbl_mst_retroinfodetails]
ADD CONSTRAINT [PK_tbl_mst_retroinfodetails]
    PRIMARY KEY CLUSTERED ([retroinfodetails_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------