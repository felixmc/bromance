
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/03/2013 15:53:33
-- Generated from EDMX file: C:\Users\Felix\Documents\GitHub\bromance\Bros\DataModel\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bromance_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CircleUser_Circle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CircleUser] DROP CONSTRAINT [FK_CircleUser_Circle];
GO
IF OBJECT_ID(N'[dbo].[FK_CircleUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CircleUser] DROP CONSTRAINT [FK_CircleUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_InterestProfile_Interest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterestProfile] DROP CONSTRAINT [FK_InterestProfile_Interest];
GO
IF OBJECT_ID(N'[dbo].[FK_InterestProfile_Profile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InterestProfile] DROP CONSTRAINT [FK_InterestProfile_Profile];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestNotificationBroRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_RequestNotification] DROP CONSTRAINT [FK_RequestNotificationBroRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTag_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTags] DROP CONSTRAINT [FK_ProductTag_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTag_Tag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTags] DROP CONSTRAINT [FK_ProductTag_Tag];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_UserPost];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_UserProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_MessagesSent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessagesSent];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CircleOwner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Circles] DROP CONSTRAINT [FK_CircleOwner];
GO
IF OBJECT_ID(N'[dbo].[FK_AlbumUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Albums] DROP CONSTRAINT [FK_AlbumUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AlbumPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_Photo] DROP CONSTRAINT [FK_AlbumPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPreferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Preferences] DROP CONSTRAINT [FK_UserPreferences];
GO
IF OBJECT_ID(N'[dbo].[FK_SentBroRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BroRequests] DROP CONSTRAINT [FK_SentBroRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_BroRequestUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BroRequests] DROP CONSTRAINT [FK_BroRequestUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SentFirstBump]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_FirstBump] DROP CONSTRAINT [FK_SentFirstBump];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentNotificationComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_CommentNotification] DROP CONSTRAINT [FK_CommentNotificationComment];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_NotificationUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfilePhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_Photo] DROP CONSTRAINT [FK_ProfilePhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_OrderUser];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProducts_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProducts] DROP CONSTRAINT [FK_OrderProducts_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProducts_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderProducts] DROP CONSTRAINT [FK_OrderProducts_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCarts] DROP CONSTRAINT [FK_ShoppingCartUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartProducts_ShoppingCart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCartProducts] DROP CONSTRAINT [FK_ShoppingCartProducts_ShoppingCart];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartProducts_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCartProducts] DROP CONSTRAINT [FK_ShoppingCartProducts_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestNotification_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_RequestNotification] DROP CONSTRAINT [FK_RequestNotification_inherits_Notification];
GO
IF OBJECT_ID(N'[dbo].[FK_Photo_inherits_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_Photo] DROP CONSTRAINT [FK_Photo_inherits_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_FirstBump_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_FirstBump] DROP CONSTRAINT [FK_FirstBump_inherits_Notification];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentNotification_inherits_Notification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications_CommentNotification] DROP CONSTRAINT [FK_CommentNotification_inherits_Notification];
GO
IF OBJECT_ID(N'[dbo].[FK_TextPost_inherits_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts_TextPost] DROP CONSTRAINT [FK_TextPost_inherits_Post];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Albums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Albums];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[BroRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BroRequests];
GO
IF OBJECT_ID(N'[dbo].[Circles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Circles];
GO
IF OBJECT_ID(N'[dbo].[Interests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Interests];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[Preferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Preferences];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCarts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCarts];
GO
IF OBJECT_ID(N'[dbo].[Notifications_RequestNotification]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications_RequestNotification];
GO
IF OBJECT_ID(N'[dbo].[Posts_Photo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts_Photo];
GO
IF OBJECT_ID(N'[dbo].[Notifications_FirstBump]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications_FirstBump];
GO
IF OBJECT_ID(N'[dbo].[Notifications_CommentNotification]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications_CommentNotification];
GO
IF OBJECT_ID(N'[dbo].[Posts_TextPost]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts_TextPost];
GO
IF OBJECT_ID(N'[dbo].[CircleUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CircleUser];
GO
IF OBJECT_ID(N'[dbo].[UserUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserUser];
GO
IF OBJECT_ID(N'[dbo].[InterestProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InterestProfile];
GO
IF OBJECT_ID(N'[dbo].[ProductTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTags];
GO
IF OBJECT_ID(N'[dbo].[OrderProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderProducts];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCartProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCartProducts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Albums'
CREATE TABLE [dbo].[Albums] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [IsBanned] bit  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsFlagged] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateUpdated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
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
CREATE TABLE [dbo].[BroRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Circles'
CREATE TABLE [dbo].[Circles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Interests'
CREATE TABLE [dbo].[Interests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [DateSent] datetime  NOT NULL,
    [DateRead] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsRead] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId1] int  NOT NULL
);
GO

-- Creating table 'Preferences'
CREATE TABLE [dbo].[Preferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
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
    [Drugs] nvarchar(max)  NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'ShoppingCarts'
CREATE TABLE [dbo].[ShoppingCarts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Notifications_RequestNotification'
CREATE TABLE [dbo].[Notifications_RequestNotification] (
    [Id] int  NOT NULL,
    [BroRequest_Id] int  NOT NULL
);
GO

-- Creating table 'Posts_Photo'
CREATE TABLE [dbo].[Posts_Photo] (
    [Caption] nvarchar(max)  NOT NULL,
    [AlbumId] int  NOT NULL,
    [ImageData] varbinary(max)  NOT NULL,
    [Id] int  NOT NULL,
    [ProfilePhotoOf_Id] int  NULL
);
GO

-- Creating table 'Notifications_FirstBump'
CREATE TABLE [dbo].[Notifications_FirstBump] (
    [UserId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Notifications_CommentNotification'
CREATE TABLE [dbo].[Notifications_CommentNotification] (
    [Id] int  NOT NULL,
    [Comment_Id] int  NOT NULL
);
GO

-- Creating table 'Posts_TextPost'
CREATE TABLE [dbo].[Posts_TextPost] (
    [Content] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'CircleUser'
CREATE TABLE [dbo].[CircleUser] (
    [JoinedCircles_Id] int  NOT NULL,
    [Members_Id] int  NOT NULL
);
GO

-- Creating table 'UserUser'
CREATE TABLE [dbo].[UserUser] (
    [BlockedByBros_Id] int  NOT NULL,
    [BlockedBros_Id] int  NOT NULL
);
GO

-- Creating table 'InterestProfile'
CREATE TABLE [dbo].[InterestProfile] (
    [Interests_Id] int  NOT NULL,
    [InterestedProfiles_Id] int  NOT NULL
);
GO

-- Creating table 'ProductTags'
CREATE TABLE [dbo].[ProductTags] (
    [Products_Id] int  NOT NULL,
    [Tags_Id] int  NOT NULL
);
GO

-- Creating table 'OrderProducts'
CREATE TABLE [dbo].[OrderProducts] (
    [Orders_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- Creating table 'ShoppingCartProducts'
CREATE TABLE [dbo].[ShoppingCartProducts] (
    [ShoppingCarts_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Albums'
ALTER TABLE [dbo].[Albums]
ADD CONSTRAINT [PK_Albums]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BroRequests'
ALTER TABLE [dbo].[BroRequests]
ADD CONSTRAINT [PK_BroRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Circles'
ALTER TABLE [dbo].[Circles]
ADD CONSTRAINT [PK_Circles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Interests'
ALTER TABLE [dbo].[Interests]
ADD CONSTRAINT [PK_Interests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Preferences'
ALTER TABLE [dbo].[Preferences]
ADD CONSTRAINT [PK_Preferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [PK_ShoppingCarts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_RequestNotification'
ALTER TABLE [dbo].[Notifications_RequestNotification]
ADD CONSTRAINT [PK_Notifications_RequestNotification]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts_Photo'
ALTER TABLE [dbo].[Posts_Photo]
ADD CONSTRAINT [PK_Posts_Photo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_FirstBump'
ALTER TABLE [dbo].[Notifications_FirstBump]
ADD CONSTRAINT [PK_Notifications_FirstBump]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications_CommentNotification'
ALTER TABLE [dbo].[Notifications_CommentNotification]
ADD CONSTRAINT [PK_Notifications_CommentNotification]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts_TextPost'
ALTER TABLE [dbo].[Posts_TextPost]
ADD CONSTRAINT [PK_Posts_TextPost]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [JoinedCircles_Id], [Members_Id] in table 'CircleUser'
ALTER TABLE [dbo].[CircleUser]
ADD CONSTRAINT [PK_CircleUser]
    PRIMARY KEY NONCLUSTERED ([JoinedCircles_Id], [Members_Id] ASC);
GO

-- Creating primary key on [BlockedByBros_Id], [BlockedBros_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [PK_UserUser]
    PRIMARY KEY NONCLUSTERED ([BlockedByBros_Id], [BlockedBros_Id] ASC);
GO

-- Creating primary key on [Interests_Id], [InterestedProfiles_Id] in table 'InterestProfile'
ALTER TABLE [dbo].[InterestProfile]
ADD CONSTRAINT [PK_InterestProfile]
    PRIMARY KEY NONCLUSTERED ([Interests_Id], [InterestedProfiles_Id] ASC);
GO

-- Creating primary key on [Products_Id], [Tags_Id] in table 'ProductTags'
ALTER TABLE [dbo].[ProductTags]
ADD CONSTRAINT [PK_ProductTags]
    PRIMARY KEY NONCLUSTERED ([Products_Id], [Tags_Id] ASC);
GO

-- Creating primary key on [Orders_Id], [Products_Id] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [PK_OrderProducts]
    PRIMARY KEY NONCLUSTERED ([Orders_Id], [Products_Id] ASC);
GO

-- Creating primary key on [ShoppingCarts_Id], [Products_Id] in table 'ShoppingCartProducts'
ALTER TABLE [dbo].[ShoppingCartProducts]
ADD CONSTRAINT [PK_ShoppingCartProducts]
    PRIMARY KEY NONCLUSTERED ([ShoppingCarts_Id], [Products_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JoinedCircles_Id] in table 'CircleUser'
ALTER TABLE [dbo].[CircleUser]
ADD CONSTRAINT [FK_CircleUser_Circle]
    FOREIGN KEY ([JoinedCircles_Id])
    REFERENCES [dbo].[Circles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Members_Id] in table 'CircleUser'
ALTER TABLE [dbo].[CircleUser]
ADD CONSTRAINT [FK_CircleUser_User]
    FOREIGN KEY ([Members_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleUser_User'
CREATE INDEX [IX_FK_CircleUser_User]
ON [dbo].[CircleUser]
    ([Members_Id]);
GO

-- Creating foreign key on [BlockedByBros_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User]
    FOREIGN KEY ([BlockedByBros_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BlockedBros_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User1]
    FOREIGN KEY ([BlockedBros_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_User1'
CREATE INDEX [IX_FK_UserUser_User1]
ON [dbo].[UserUser]
    ([BlockedBros_Id]);
GO

-- Creating foreign key on [Interests_Id] in table 'InterestProfile'
ALTER TABLE [dbo].[InterestProfile]
ADD CONSTRAINT [FK_InterestProfile_Interest]
    FOREIGN KEY ([Interests_Id])
    REFERENCES [dbo].[Interests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InterestedProfiles_Id] in table 'InterestProfile'
ALTER TABLE [dbo].[InterestProfile]
ADD CONSTRAINT [FK_InterestProfile_Profile]
    FOREIGN KEY ([InterestedProfiles_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InterestProfile_Profile'
CREATE INDEX [IX_FK_InterestProfile_Profile]
ON [dbo].[InterestProfile]
    ([InterestedProfiles_Id]);
GO

-- Creating foreign key on [BroRequest_Id] in table 'Notifications_RequestNotification'
ALTER TABLE [dbo].[Notifications_RequestNotification]
ADD CONSTRAINT [FK_RequestNotificationBroRequest]
    FOREIGN KEY ([BroRequest_Id])
    REFERENCES [dbo].[BroRequests]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestNotificationBroRequest'
CREATE INDEX [IX_FK_RequestNotificationBroRequest]
ON [dbo].[Notifications_RequestNotification]
    ([BroRequest_Id]);
GO

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'
CREATE INDEX [IX_FK_ProductCategory]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [Products_Id] in table 'ProductTags'
ALTER TABLE [dbo].[ProductTags]
ADD CONSTRAINT [FK_ProductTag_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_Id] in table 'ProductTags'
ALTER TABLE [dbo].[ProductTags]
ADD CONSTRAINT [FK_ProductTag_Tag]
    FOREIGN KEY ([Tags_Id])
    REFERENCES [dbo].[Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTag_Tag'
CREATE INDEX [IX_FK_ProductTag_Tag]
ON [dbo].[ProductTags]
    ([Tags_Id]);
GO

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [UserId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_UserPost]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPost'
CREATE INDEX [IX_FK_UserPost]
ON [dbo].[Posts]
    ([UserId]);
GO

-- Creating foreign key on [User_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_UserProfile]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfile'
CREATE INDEX [IX_FK_UserProfile]
ON [dbo].[Profiles]
    ([User_Id]);
GO

-- Creating foreign key on [UserId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessagesSent]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessagesSent'
CREATE INDEX [IX_FK_MessagesSent]
ON [dbo].[Messages]
    ([UserId]);
GO

-- Creating foreign key on [UserId1] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'
CREATE INDEX [IX_FK_MessageUser]
ON [dbo].[Messages]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Circles'
ALTER TABLE [dbo].[Circles]
ADD CONSTRAINT [FK_CircleOwner]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleOwner'
CREATE INDEX [IX_FK_CircleOwner]
ON [dbo].[Circles]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Albums'
ALTER TABLE [dbo].[Albums]
ADD CONSTRAINT [FK_AlbumUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumUser'
CREATE INDEX [IX_FK_AlbumUser]
ON [dbo].[Albums]
    ([UserId]);
GO

-- Creating foreign key on [AlbumId] in table 'Posts_Photo'
ALTER TABLE [dbo].[Posts_Photo]
ADD CONSTRAINT [FK_AlbumPhoto]
    FOREIGN KEY ([AlbumId])
    REFERENCES [dbo].[Albums]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumPhoto'
CREATE INDEX [IX_FK_AlbumPhoto]
ON [dbo].[Posts_Photo]
    ([AlbumId]);
GO

-- Creating foreign key on [UserId] in table 'Preferences'
ALTER TABLE [dbo].[Preferences]
ADD CONSTRAINT [FK_UserPreferences]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPreferences'
CREATE INDEX [IX_FK_UserPreferences]
ON [dbo].[Preferences]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'BroRequests'
ALTER TABLE [dbo].[BroRequests]
ADD CONSTRAINT [FK_SentBroRequest]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentBroRequest'
CREATE INDEX [IX_FK_SentBroRequest]
ON [dbo].[BroRequests]
    ([UserId]);
GO

-- Creating foreign key on [UserId1] in table 'BroRequests'
ALTER TABLE [dbo].[BroRequests]
ADD CONSTRAINT [FK_BroRequestUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BroRequestUser'
CREATE INDEX [IX_FK_BroRequestUser]
ON [dbo].[BroRequests]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Notifications_FirstBump'
ALTER TABLE [dbo].[Notifications_FirstBump]
ADD CONSTRAINT [FK_SentFirstBump]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentFirstBump'
CREATE INDEX [IX_FK_SentFirstBump]
ON [dbo].[Notifications_FirstBump]
    ([UserId]);
GO

-- Creating foreign key on [Comment_Id] in table 'Notifications_CommentNotification'
ALTER TABLE [dbo].[Notifications_CommentNotification]
ADD CONSTRAINT [FK_CommentNotificationComment]
    FOREIGN KEY ([Comment_Id])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentNotificationComment'
CREATE INDEX [IX_FK_CommentNotificationComment]
ON [dbo].[Notifications_CommentNotification]
    ([Comment_Id]);
GO

-- Creating foreign key on [UserId1] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_NotificationUser]
    FOREIGN KEY ([UserId1])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationUser'
CREATE INDEX [IX_FK_NotificationUser]
ON [dbo].[Notifications]
    ([UserId1]);
GO

-- Creating foreign key on [UserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUser'
CREATE INDEX [IX_FK_CommentUser]
ON [dbo].[Comments]
    ([UserId]);
GO

-- Creating foreign key on [ProfilePhotoOf_Id] in table 'Posts_Photo'
ALTER TABLE [dbo].[Posts_Photo]
ADD CONSTRAINT [FK_ProfilePhoto]
    FOREIGN KEY ([ProfilePhotoOf_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfilePhoto'
CREATE INDEX [IX_FK_ProfilePhoto]
ON [dbo].[Posts_Photo]
    ([ProfilePhotoOf_Id]);
GO

-- Creating foreign key on [UserId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_OrderUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderUser'
CREATE INDEX [IX_FK_OrderUser]
ON [dbo].[Orders]
    ([UserId]);
GO

-- Creating foreign key on [Orders_Id] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProducts_Order]
    FOREIGN KEY ([Orders_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'OrderProducts'
ALTER TABLE [dbo].[OrderProducts]
ADD CONSTRAINT [FK_OrderProducts_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProducts_Product'
CREATE INDEX [IX_FK_OrderProducts_Product]
ON [dbo].[OrderProducts]
    ([Products_Id]);
GO

-- Creating foreign key on [User_Id] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK_ShoppingCartUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCartUser'
CREATE INDEX [IX_FK_ShoppingCartUser]
ON [dbo].[ShoppingCarts]
    ([User_Id]);
GO

-- Creating foreign key on [ShoppingCarts_Id] in table 'ShoppingCartProducts'
ALTER TABLE [dbo].[ShoppingCartProducts]
ADD CONSTRAINT [FK_ShoppingCartProducts_ShoppingCart]
    FOREIGN KEY ([ShoppingCarts_Id])
    REFERENCES [dbo].[ShoppingCarts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'ShoppingCartProducts'
ALTER TABLE [dbo].[ShoppingCartProducts]
ADD CONSTRAINT [FK_ShoppingCartProducts_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCartProducts_Product'
CREATE INDEX [IX_FK_ShoppingCartProducts_Product]
ON [dbo].[ShoppingCartProducts]
    ([Products_Id]);
GO

-- Creating foreign key on [Id] in table 'Notifications_RequestNotification'
ALTER TABLE [dbo].[Notifications_RequestNotification]
ADD CONSTRAINT [FK_RequestNotification_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Posts_Photo'
ALTER TABLE [dbo].[Posts_Photo]
ADD CONSTRAINT [FK_Photo_inherits_Post]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Notifications_FirstBump'
ALTER TABLE [dbo].[Notifications_FirstBump]
ADD CONSTRAINT [FK_FirstBump_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Notifications_CommentNotification'
ALTER TABLE [dbo].[Notifications_CommentNotification]
ADD CONSTRAINT [FK_CommentNotification_inherits_Notification]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Posts_TextPost'
ALTER TABLE [dbo].[Posts_TextPost]
ADD CONSTRAINT [FK_TextPost_inherits_Post]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------