
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/23/2012 18:50:36
-- Generated from EDMX file: d:\users\james\documents\Projects\Visual Studio 2010\Review-Site\Review-Site\Models\ReviewEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ReviewSite];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Article_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article] DROP CONSTRAINT [FK_Article_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article] DROP CONSTRAINT [FK_Article_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Category_CategoryColour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Category] DROP CONSTRAINT [FK_Category_CategoryColour];
GO
IF OBJECT_ID(N'[dbo].[FK_GridElement_Color]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GridElement] DROP CONSTRAINT [FK_GridElement_Color];
GO
IF OBJECT_ID(N'[dbo].[FK_GridElement_Grid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GridElement] DROP CONSTRAINT [FK_GridElement_Grid];
GO
IF OBJECT_ID(N'[dbo].[FK_GridElement_Resource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GridElement] DROP CONSTRAINT [FK_GridElement_Resource];
GO
IF OBJECT_ID(N'[dbo].[FK_HomeElement_Article]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GridElement] DROP CONSTRAINT [FK_HomeElement_Article];
GO
IF OBJECT_ID(N'[dbo].[FK_Resource_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resource] DROP CONSTRAINT [FK_Resource_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Color]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Color];
GO
IF OBJECT_ID(N'[dbo].[Grid]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Grid];
GO
IF OBJECT_ID(N'[dbo].[GridElement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GridElement];
GO
IF OBJECT_ID(N'[dbo].[Resource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resource];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ID] uniqueidentifier  NOT NULL,
    [CategoryID] uniqueidentifier  NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(60)  NOT NULL,
    [ShortDescription] nvarchar(150)  NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [LastModified] datetime  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(150)  NOT NULL,
    [ColorID] uniqueidentifier  NULL,
    [IsSystemCategory] tinyint  NOT NULL
);
GO

-- Creating table 'Colors'
CREATE TABLE [dbo].[Colors] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(60)  NOT NULL,
    [Value] nchar(6)  NOT NULL
);
GO

-- Creating table 'Grids'
CREATE TABLE [dbo].[Grids] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Alias] nchar(60)  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'GridElements'
CREATE TABLE [dbo].[GridElements] (
    [ID] uniqueidentifier  NOT NULL,
    [BorderColorID] uniqueidentifier  NOT NULL,
    [ArticleID] uniqueidentifier  NULL,
    [GridID] uniqueidentifier  NOT NULL,
    [ImageID] uniqueidentifier  NOT NULL,
    [SizeClass] nchar(10)  NULL,
    [HeadingClass] nchar(10)  NULL,
    [HeadingText] nvarchar(150)  NULL,
    [UseHeadingText] tinyint  NOT NULL,
    [CustomDestination] nvarchar(200)  NULL,
    [UseCustomDestination] tinyint  NOT NULL
);
GO

-- Creating table 'Resources'
CREATE TABLE [dbo].[Resources] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatorID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(100)  NOT NULL,
    [Type] nchar(20)  NOT NULL,
    [DateAdded] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] uniqueidentifier  NOT NULL,
    [Username] nvarchar(150)  NOT NULL,
    [Password] binary(32)  NOT NULL,
    [AuthWithAD] tinyint  NOT NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Colors'
ALTER TABLE [dbo].[Colors]
ADD CONSTRAINT [PK_Colors]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Grids'
ALTER TABLE [dbo].[Grids]
ADD CONSTRAINT [PK_Grids]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GridElements'
ALTER TABLE [dbo].[GridElements]
ADD CONSTRAINT [PK_GridElements]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [PK_Resources]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_Article_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Categories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_Category'
CREATE INDEX [IX_FK_Article_Category]
ON [dbo].[Articles]
    ([CategoryID]);
GO

-- Creating foreign key on [AuthorID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_Article_User]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_User'
CREATE INDEX [IX_FK_Article_User]
ON [dbo].[Articles]
    ([AuthorID]);
GO

-- Creating foreign key on [ArticleID] in table 'GridElements'
ALTER TABLE [dbo].[GridElements]
ADD CONSTRAINT [FK_HomeElement_Article]
    FOREIGN KEY ([ArticleID])
    REFERENCES [dbo].[Articles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HomeElement_Article'
CREATE INDEX [IX_FK_HomeElement_Article]
ON [dbo].[GridElements]
    ([ArticleID]);
GO

-- Creating foreign key on [ColorID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Category_CategoryColour]
    FOREIGN KEY ([ColorID])
    REFERENCES [dbo].[Colors]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_CategoryColour'
CREATE INDEX [IX_FK_Category_CategoryColour]
ON [dbo].[Categories]
    ([ColorID]);
GO

-- Creating foreign key on [BorderColorID] in table 'GridElements'
ALTER TABLE [dbo].[GridElements]
ADD CONSTRAINT [FK_GridElement_Color]
    FOREIGN KEY ([BorderColorID])
    REFERENCES [dbo].[Colors]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GridElement_Color'
CREATE INDEX [IX_FK_GridElement_Color]
ON [dbo].[GridElements]
    ([BorderColorID]);
GO

-- Creating foreign key on [GridID] in table 'GridElements'
ALTER TABLE [dbo].[GridElements]
ADD CONSTRAINT [FK_GridElement_Grid]
    FOREIGN KEY ([GridID])
    REFERENCES [dbo].[Grids]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GridElement_Grid'
CREATE INDEX [IX_FK_GridElement_Grid]
ON [dbo].[GridElements]
    ([GridID]);
GO

-- Creating foreign key on [ImageID] in table 'GridElements'
ALTER TABLE [dbo].[GridElements]
ADD CONSTRAINT [FK_GridElement_Resource]
    FOREIGN KEY ([ImageID])
    REFERENCES [dbo].[Resources]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GridElement_Resource'
CREATE INDEX [IX_FK_GridElement_Resource]
ON [dbo].[GridElements]
    ([ImageID]);
GO

-- Creating foreign key on [CreatorID] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [FK_Resource_User]
    FOREIGN KEY ([CreatorID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Resource_User'
CREATE INDEX [IX_FK_Resource_User]
ON [dbo].[Resources]
    ([CreatorID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------