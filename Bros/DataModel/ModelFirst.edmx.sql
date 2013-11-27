
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2013 00:30:42
-- Generated from EDMX file: C:\Users\Felix\Documents\GitHub\bromance\Bros\DataModel\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bromance_DB];
GO
IF SCHEMA_ID(N'bromance_DB') IS NULL EXECUTE(N'CREATE SCHEMA [bromance_DB]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[bromance_DB].[FK_CircleUser_Circle]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[CircleUser] DROP CONSTRAINT [FK_CircleUser_Circle];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_CircleUser_User]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[CircleUser] DROP CONSTRAINT [FK_CircleUser_User];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_UserUser_User]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[UserUser] DROP CONSTRAINT [FK_UserUser_User];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_UserUser_User1]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[UserUser] DROP CONSTRAINT [FK_UserUser_User1];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_InterestProfile_Interest]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[InterestProfile] DROP CONSTRAINT [FK_InterestProfile_Interest];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_InterestProfile_Profile]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[InterestProfile] DROP CONSTRAINT [FK_InterestProfile_Profile];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_RequestNotificationBroRequest]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_RequestNotification] DROP CONSTRAINT [FK_RequestNotificationBroRequest];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_ProductCategory]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Products] DROP CONSTRAINT [FK_ProductCategory];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_ProductTag_Product]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[ProductTags] DROP CONSTRAINT [FK_ProductTag_Product];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_ProductTag_Tag]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[ProductTags] DROP CONSTRAINT [FK_ProductTag_Tag];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_UserPost]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Posts] DROP CONSTRAINT [FK_UserPost];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_UserProfile]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Users] DROP CONSTRAINT [FK_UserProfile];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_MessagesSent]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Messages] DROP CONSTRAINT [FK_MessagesSent];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_MessageUser]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Messages] DROP CONSTRAINT [FK_MessageUser];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_CircleOwner]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Circles] DROP CONSTRAINT [FK_CircleOwner];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_AlbumUser]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Albums] DROP CONSTRAINT [FK_AlbumUser];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_AlbumPhoto]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Posts_Photo] DROP CONSTRAINT [FK_AlbumPhoto];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_UserPreferences]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Preferences] DROP CONSTRAINT [FK_UserPreferences];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_SentBroRequest]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[BroRequests] DROP CONSTRAINT [FK_SentBroRequest];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_BroRequestUser]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[BroRequests] DROP CONSTRAINT [FK_BroRequestUser];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_SentFirstBump]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_FirstBump] DROP CONSTRAINT [FK_SentFirstBump];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_CommentNotificationComment]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_CommentNotification] DROP CONSTRAINT [FK_CommentNotificationComment];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_NotificationUser]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications] DROP CONSTRAINT [FK_NotificationUser];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_CommentUser]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Comments] DROP CONSTRAINT [FK_CommentUser];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_ProfilePhoto]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Posts_Photo] DROP CONSTRAINT [FK_ProfilePhoto];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_RequestNotification_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_RequestNotification] DROP CONSTRAINT [FK_RequestNotification_inherits_Notification];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_Photo_inherits_Post]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Posts_Photo] DROP CONSTRAINT [FK_Photo_inherits_Post];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_FirstBump_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_FirstBump] DROP CONSTRAINT [FK_FirstBump_inherits_Notification];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_CommentNotification_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Notifications_CommentNotification] DROP CONSTRAINT [FK_CommentNotification_inherits_Notification];
GO
IF OBJECT_ID(N'[bromance_DB].[FK_TextPost_inherits_Post]', 'F') IS NOT NULL
    ALTER TABLE [bromance_DB].[Posts_TextPost] DROP CONSTRAINT [FK_TextPost_inherits_Post];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[bromance_DB].[Albums]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Albums];
GO
IF OBJECT_ID(N'[bromance_DB].[Users]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Users];
GO
IF OBJECT_ID(N'[bromance_DB].[Posts]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Posts];
GO
IF OBJECT_ID(N'[bromance_DB].[Comments]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Comments];
GO
IF OBJECT_ID(N'[bromance_DB].[BroRequests]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[BroRequests];
GO
IF OBJECT_ID(N'[bromance_DB].[Circles]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Circles];
GO
IF OBJECT_ID(N'[bromance_DB].[Interests]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Interests];
GO
IF OBJECT_ID(N'[bromance_DB].[Messages]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Messages];
GO
IF OBJECT_ID(N'[bromance_DB].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Notifications];
GO
IF OBJECT_ID(N'[bromance_DB].[Preferences]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Preferences];
GO
IF OBJECT_ID(N'[bromance_DB].[Products]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Products];
GO
IF OBJECT_ID(N'[bromance_DB].[Tags]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Tags];
GO
IF OBJECT_ID(N'[bromance_DB].[Categories]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Categories];
GO
IF OBJECT_ID(N'[bromance_DB].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Profiles];
GO
IF OBJECT_ID(N'[bromance_DB].[Notifications_RequestNotification]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Notifications_RequestNotification];
GO
IF OBJECT_ID(N'[bromance_DB].[Posts_Photo]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Posts_Photo];
GO
IF OBJECT_ID(N'[bromance_DB].[Notifications_FirstBump]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Notifications_FirstBump];
GO
IF OBJECT_ID(N'[bromance_DB].[Notifications_CommentNotification]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Notifications_CommentNotification];
GO
IF OBJECT_ID(N'[bromance_DB].[Posts_TextPost]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[Posts_TextPost];
GO
IF OBJECT_ID(N'[bromance_DB].[CircleUser]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[CircleUser];
GO
IF OBJECT_ID(N'[bromance_DB].[UserUser]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[UserUser];
GO
IF OBJECT_ID(N'[bromance_DB].[InterestProfile]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[InterestProfile];
GO
IF OBJECT_ID(N'[bromance_DB].[ProductTags]', 'U') IS NOT NULL
    DROP TABLE [bromance_DB].[ProductTags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Albums'
CREATE TABLE [bromance_DB].[Albums] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [bromance_DB].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [IsBanned] bit  NOT NULL,
    [Profile_Id] int  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [bromance_DB].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsFlagged] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [bromance_DB].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [IsFlagged] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [PostId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'BroRequests'
CREATE TABLE [bromance_DB].[BroRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Circles'
CREATE TABLE [bromance_DB].[Circles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Interests'
CREATE TABLE [bromance_DB].[Interests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [bromance_DB].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [DateSent] datetime  NOT NULL,
    [DateRead] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [bromance_DB].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsRead] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Preferences'
CREATE TABLE [bromance_DB].[Preferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [bromance_DB].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Image] nvarchar(max)  NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [bromance_DB].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [bromance_DB].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [bromance_DB].[Profiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Pets] nvarchar(max)  NULL,
    [Religion] nvarchar(max)  NULL,
    [Job] nvarchar(max)  NULL,
    [Education] nvarchar(max)  NULL,
    [Ethnicity] nvarchar(max)  NULL,
    [Athleticism] nvarchar(max)  NULL,
    [SexualOrientation] nvarchar(max)  NULL,
    [MarriageStatus] nvarchar(max)  NULL,
    [Children] nvarchar(max)  NULL,
    [Smokes] nvarchar(max)  NULL,
    [Drinks] nvarchar(max)  NULL,
    [Drugs] nvarchar(max)  NULL
);
GO

-- Creating table 'Notifications_RequestNotification'
CREATE TABLE [bromance_DB].[Notifications_RequestNotification] (
    [Id] int  NOT NULL,
    [BroRequest_Id] int  NOT NULL
);
GO

-- Creating table 'Posts_Photo'
CREATE TABLE [bromance_DB].[Posts_Photo] (
    [Caption] nvarchar(max)  NOT NULL,
    [AlbumId] int  NOT NULL,
    [Id] int  NOT NULL,
    [ProfilePhotoOf_Id] int  NULL
);
GO

-- Creating table 'Notifications_FirstBump'
CREATE TABLE [bromance_DB].[Notifications_FirstBump] (
    [UserId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Notifications_CommentNotification'
CREATE TABLE [bromance_DB].[Notifications_CommentNotification] (
    [Id] int  NOT NULL,
    [Comment_Id] int  NOT NULL
);
GO

-- Creating table 'Posts_TextPost'
CREATE TABLE [bromance_DB].[Posts_TextPost] (
    [Content] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'CircleUser'
CREATE TABLE [bromance_DB].[CircleUser] (
    [JoinedCircles_Id] int  NOT NULL,
    [Members_Id] int  NOT NULL
);
GO

-- Creating table 'UserUser'
CREATE TABLE [bromance_DB].[UserUser] (
    [BlockedByBros_Id] int  NOT NULL,
    [BlockedBros_Id] int  NOT NULL
);
GO

-- Creating table 'InterestProfile'
CREATE TABLE [bromance_DB].[InterestProfile] (
    [Interests_Id] int  NOT NULL,
    [InterestedProfiles_Id] int  NOT NULL
);
GO

-- Creating table 'ProductTags'
CREATE TABLE [bromance_DB].[ProductTags] (
    [Products_Id] int  NOT NULL,
    [Tags_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Albums'
ALTER TABLE [bromance_DB].[Albums]
ADD CONSTRAINT [PK_Albums]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [bromance_DB].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [bromance_DB].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [bromance_DB].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BroRequests'
ALTER TABLE [bromance_DB].[BroRequests]
ADD CONSTRAINT [PK_BroRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Circles'
ALTER TABLE [bromance_DB].[Circles]
ADD CONSTRAINT [PK_Circles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Interests'
ALTER TABLE [bromance_DB].[Interests]
ADD CONSTRAINT [PK_Interests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [bromance_DB].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [bromance_DB].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Preferences'
ALTER TABLE [bromance_DB].[Preferences]
ADD CONSTRAINT [PK_Preferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [bromance_DB].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [bromance_DB].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [bromance_DB].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profiles'
ALTER TABLE [bromance_DB].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_RequestNotification'
ALTER TABLE [bromance_DB].[Notifications_RequestNotification]
ADD CONSTRAINT [PK_Notifications_RequestNotification]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts_Photo'
ALTER TABLE [bromance_DB].[Posts_Photo]
ADD CONSTRAINT [PK_Posts_Photo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_FirstBump'
ALTER TABLE [bromance_DB].[Notifications_FirstBump]
ADD CONSTRAINT [PK_Notifications_FirstBump]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_CommentNotification'
ALTER TABLE [bromance_DB].[Notifications_CommentNotification]
ADD CONSTRAINT [PK_Notifications_CommentNotification]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts_TextPost'
ALTER TABLE [bromance_DB].[Posts_TextPost]
ADD CONSTRAINT [PK_Posts_TextPost]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [JoinedCircles_Id], [Members_Id] in table 'CircleUser'
ALTER TABLE [bromance_DB].[CircleUser]
ADD CONSTRAINT [PK_CircleUser]
    PRIMARY KEY NONCLUSTERED ([JoinedCircles_Id], [Members_Id] ASC);
GO

-- Creating primary key on [BlockedByBros_Id], [BlockedBros_Id] in table 'UserUser'
ALTER TABLE [bromance_DB].[UserUser]
ADD CONSTRAINT [PK_UserUser]
    PRIMARY KEY NONCLUSTERED ([BlockedByBros_Id], [BlockedBros_Id] ASC);
GO

-- Creating primary key on [Interests_Id], [InterestedProfiles_Id] in table 'InterestProfile'
ALTER TABLE [bromance_DB].[InterestProfile]
ADD CONSTRAINT [PK_InterestProfile]
    PRIMARY KEY NONCLUSTERED ([Interests_Id], [InterestedProfiles_Id] ASC);
GO

-- Creating primary key on [Products_Id], [Tags_Id] in table 'ProductTags'
ALTER TABLE [bromance_DB].[ProductTags]
ADD CONSTRAINT [PK_ProductTags]
    PRIMARY KEY NONCLUSTERED ([Products_Id], [Tags_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JoinedCircles_Id] in table 'CircleUser'
ALTER TABLE [bromance_DB].[CircleUser]
ADD CONSTRAINT [FK_CircleUser_Circle]
    FOREIGN KEY ([JoinedCircles_Id])
    REFERENCES [bromance_DB].[Circles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Members_Id] in table 'CircleUser'
ALTER TABLE [bromance_DB].[CircleUser]
ADD CONSTRAINT [FK_CircleUser_User]
    FOREIGN KEY ([Members_Id])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleUser_User'
CREATE INDEX [IX_FK_CircleUser_User]
ON [bromance_DB].[CircleUser]
    ([Members_Id]);
GO

-- Creating foreign key on [BlockedByBros_Id] in table 'UserUser'
ALTER TABLE [bromance_DB].[UserUser]
ADD CONSTRAINT [FK_UserUser_User]
    FOREIGN KEY ([BlockedByBros_Id])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BlockedBros_Id] in table 'UserUser'
ALTER TABLE [bromance_DB].[UserUser]
ADD CONSTRAINT [FK_UserUser_User1]
    FOREIGN KEY ([BlockedBros_Id])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_User1'
CREATE INDEX [IX_FK_UserUser_User1]
ON [bromance_DB].[UserUser]
    ([BlockedBros_Id]);
GO

-- Creating foreign key on [Interests_Id] in table 'InterestProfile'
ALTER TABLE [bromance_DB].[InterestProfile]
ADD CONSTRAINT [FK_InterestProfile_Interest]
    FOREIGN KEY ([Interests_Id])
    REFERENCES [bromance_DB].[Interests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InterestedProfiles_Id] in table 'InterestProfile'
ALTER TABLE [bromance_DB].[InterestProfile]
ADD CONSTRAINT [FK_InterestProfile_Profile]
    FOREIGN KEY ([InterestedProfiles_Id])
    REFERENCES [bromance_DB].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InterestProfile_Profile'
CREATE INDEX [IX_FK_InterestProfile_Profile]
ON [bromance_DB].[InterestProfile]
    ([InterestedProfiles_Id]);
GO

-- Creating foreign key on [BroRequest_Id] in table 'Notifications_RequestNotification'
ALTER TABLE [bromance_DB].[Notifications_RequestNotification]
ADD CONSTRAINT [FK_RequestNotificationBroRequest]
    FOREIGN KEY ([BroRequest_Id])
    REFERENCES [bromance_DB].[BroRequests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestNotificationBroRequest'
CREATE INDEX [IX_FK_RequestNotificationBroRequest]
ON [bromance_DB].[Notifications_RequestNotification]
    ([BroRequest_Id]);
GO

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [bromance_DB].[Products]
ADD CONSTRAINT [FK_ProductCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [bromance_DB].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'
CREATE INDEX [IX_FK_ProductCategory]
ON [bromance_DB].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [Products_Id] in table 'ProductTags'
ALTER TABLE [bromance_DB].[ProductTags]
ADD CONSTRAINT [FK_ProductTag_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [bromance_DB].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_Id] in table 'ProductTags'
ALTER TABLE [bromance_DB].[ProductTags]
ADD CONSTRAINT [FK_ProductTag_Tag]
    FOREIGN KEY ([Tags_Id])
    REFERENCES [bromance_DB].[Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTag_Tag'
CREATE INDEX [IX_FK_ProductTag_Tag]
ON [bromance_DB].[ProductTags]
    ([Tags_Id]);
GO

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [bromance_DB].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [bromance_DB].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [bromance_DB].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [UserId] in table 'Posts'
ALTER TABLE [bromance_DB].[Posts]
ADD CONSTRAINT [FK_UserPost]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPost'
CREATE INDEX [IX_FK_UserPost]
ON [bromance_DB].[Posts]
    ([UserId]);
GO

-- Creating foreign key on [Profile_Id] in table 'Users'
ALTER TABLE [bromance_DB].[Users]
ADD CONSTRAINT [FK_UserProfile]
    FOREIGN KEY ([Profile_Id])
    REFERENCES [bromance_DB].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfile'
CREATE INDEX [IX_FK_UserProfile]
ON [bromance_DB].[Users]
    ([Profile_Id]);
GO

-- Creating foreign key on [UserId] in table 'Messages'
ALTER TABLE [bromance_DB].[Messages]
ADD CONSTRAINT [FK_MessagesSent]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessagesSent'
CREATE INDEX [IX_FK_MessagesSent]
ON [bromance_DB].[Messages]
    ([UserId]);
GO

-- Creating foreign key on [UserId1] in table 'Messages'
ALTER TABLE [bromance_DB].[Messages]
ADD CONSTRAINT [FK_MessageUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'
CREATE INDEX [IX_FK_MessageUser]
ON [bromance_DB].[Messages]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Circles'
ALTER TABLE [bromance_DB].[Circles]
ADD CONSTRAINT [FK_CircleOwner]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleOwner'
CREATE INDEX [IX_FK_CircleOwner]
ON [bromance_DB].[Circles]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Albums'
ALTER TABLE [bromance_DB].[Albums]
ADD CONSTRAINT [FK_AlbumUser]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumUser'
CREATE INDEX [IX_FK_AlbumUser]
ON [bromance_DB].[Albums]
    ([UserId]);
GO

-- Creating foreign key on [AlbumId] in table 'Posts_Photo'
ALTER TABLE [bromance_DB].[Posts_Photo]
ADD CONSTRAINT [FK_AlbumPhoto]
    FOREIGN KEY ([AlbumId])
    REFERENCES [bromance_DB].[Albums]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumPhoto'
CREATE INDEX [IX_FK_AlbumPhoto]
ON [bromance_DB].[Posts_Photo]
    ([AlbumId]);
GO

-- Creating foreign key on [UserId] in table 'Preferences'
ALTER TABLE [bromance_DB].[Preferences]
ADD CONSTRAINT [FK_UserPreferences]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPreferences'
CREATE INDEX [IX_FK_UserPreferences]
ON [bromance_DB].[Preferences]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'BroRequests'
ALTER TABLE [bromance_DB].[BroRequests]
ADD CONSTRAINT [FK_SentBroRequest]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentBroRequest'
CREATE INDEX [IX_FK_SentBroRequest]
ON [bromance_DB].[BroRequests]
    ([UserId]);
GO

-- Creating foreign key on [UserId1] in table 'BroRequests'
ALTER TABLE [bromance_DB].[BroRequests]
ADD CONSTRAINT [FK_BroRequestUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BroRequestUser'
CREATE INDEX [IX_FK_BroRequestUser]
ON [bromance_DB].[BroRequests]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Notifications_FirstBump'
ALTER TABLE [bromance_DB].[Notifications_FirstBump]
ADD CONSTRAINT [FK_SentFirstBump]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentFirstBump'
CREATE INDEX [IX_FK_SentFirstBump]
ON [bromance_DB].[Notifications_FirstBump]
    ([UserId]);
GO

-- Creating foreign key on [Comment_Id] in table 'Notifications_CommentNotification'
ALTER TABLE [bromance_DB].[Notifications_CommentNotification]
ADD CONSTRAINT [FK_CommentNotificationComment]
    FOREIGN KEY ([Comment_Id])
    REFERENCES [bromance_DB].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentNotificationComment'
CREATE INDEX [IX_FK_CommentNotificationComment]
ON [bromance_DB].[Notifications_CommentNotification]
    ([Comment_Id]);
GO

-- Creating foreign key on [UserId1] in table 'Notifications'
ALTER TABLE [bromance_DB].[Notifications]
ADD CONSTRAINT [FK_NotificationUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationUser'
CREATE INDEX [IX_FK_NotificationUser]
ON [bromance_DB].[Notifications]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Comments'
ALTER TABLE [bromance_DB].[Comments]
ADD CONSTRAINT [FK_CommentUser]
    FOREIGN KEY ([UserId])
    REFERENCES [bromance_DB].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUser'
CREATE INDEX [IX_FK_CommentUser]
ON [bromance_DB].[Comments]
    ([UserId]);
GO

-- Creating foreign key on [ProfilePhotoOf_Id] in table 'Posts_Photo'
ALTER TABLE [bromance_DB].[Posts_Photo]
ADD CONSTRAINT [FK_ProfilePhoto]
    FOREIGN KEY ([ProfilePhotoOf_Id])
    REFERENCES [bromance_DB].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfilePhoto'
CREATE INDEX [IX_FK_ProfilePhoto]
ON [bromance_DB].[Posts_Photo]
    ([ProfilePhotoOf_Id]);
GO

-- Creating foreign key on [Id] in table 'Notifications_RequestNotification'
ALTER TABLE [bromance_DB].[Notifications_RequestNotification]
ADD CONSTRAINT [FK_RequestNotification_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [bromance_DB].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Posts_Photo'
ALTER TABLE [bromance_DB].[Posts_Photo]
ADD CONSTRAINT [FK_Photo_inherits_Post]
    FOREIGN KEY ([Id])
    REFERENCES [bromance_DB].[Posts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Notifications_FirstBump'
ALTER TABLE [bromance_DB].[Notifications_FirstBump]
ADD CONSTRAINT [FK_FirstBump_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [bromance_DB].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Notifications_CommentNotification'
ALTER TABLE [bromance_DB].[Notifications_CommentNotification]
ADD CONSTRAINT [FK_CommentNotification_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [bromance_DB].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Posts_TextPost'
ALTER TABLE [bromance_DB].[Posts_TextPost]
ADD CONSTRAINT [FK_TextPost_inherits_Post]
    FOREIGN KEY ([Id])
    REFERENCES [bromance_DB].[Posts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------