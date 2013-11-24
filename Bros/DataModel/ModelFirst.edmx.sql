



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 11/23/2013 23:59:13
-- Generated from EDMX file: C:\Users\Felix\Documents\GitHub\bromance\Bros\DataModel\ModelFirst.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `bromance`;
CREATE DATABASE `bromance`;
USE `bromance`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Albums`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (1000) NOT NULL, 
	`DateCreated` datetime NOT NULL, 
	`UserId` int NOT NULL);

ALTER TABLE `Albums` ADD PRIMARY KEY (Id);




CREATE TABLE `Users`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DateCreated` datetime NOT NULL, 
	`Email` varchar (1000) NOT NULL, 
	`Password` longblob NOT NULL, 
	`Salt` longblob NOT NULL, 
	`Profile_Id` int NOT NULL);

ALTER TABLE `Users` ADD PRIMARY KEY (Id);




CREATE TABLE `Posts`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`IsFlagged` bool NOT NULL, 
	`DateCreated` datetime NOT NULL, 
	`DateUpdated` datetime NOT NULL, 
	`UserId` int NOT NULL);

ALTER TABLE `Posts` ADD PRIMARY KEY (Id);




CREATE TABLE `Comments`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Content` varchar (1000) NOT NULL, 
	`IsFlagged` bool NOT NULL, 
	`DateCreated` datetime NOT NULL, 
	`PostId` int NOT NULL, 
	`UserId` int NOT NULL);

ALTER TABLE `Comments` ADD PRIMARY KEY (Id);




CREATE TABLE `BroRequests`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Message` varchar (1000) NOT NULL, 
	`DateCreated` datetime NOT NULL, 
	`UserId` int NOT NULL, 
	`UserId1` int NOT NULL);

ALTER TABLE `BroRequests` ADD PRIMARY KEY (Id);




CREATE TABLE `Circles`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (1000) NOT NULL, 
	`UserId` int NOT NULL);

ALTER TABLE `Circles` ADD PRIMARY KEY (Id);




CREATE TABLE `Interests`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (1000) NOT NULL);

ALTER TABLE `Interests` ADD PRIMARY KEY (Id);




CREATE TABLE `Messages`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Content` varchar (1000) NOT NULL, 
	`DateSent` datetime NOT NULL, 
	`DateRead` datetime NOT NULL, 
	`UserId` int NOT NULL, 
	`UserId1` int NOT NULL);

ALTER TABLE `Messages` ADD PRIMARY KEY (Id);




CREATE TABLE `Notifications`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`IsRead` bool NOT NULL, 
	`DateCreated` datetime NOT NULL, 
	`UserId1` int NOT NULL);

ALTER TABLE `Notifications` ADD PRIMARY KEY (Id);




CREATE TABLE `Preferences`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Key` varchar (1000) NOT NULL, 
	`Value` varchar (1000) NOT NULL, 
	`UserId` int NOT NULL);

ALTER TABLE `Preferences` ADD PRIMARY KEY (Id);




CREATE TABLE `Products`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (1000) NOT NULL, 
	`Price` decimal( 10, 2 )  NOT NULL, 
	`Image` varchar (1000), 
	`CategoryId` int NOT NULL);

ALTER TABLE `Products` ADD PRIMARY KEY (Id);




CREATE TABLE `Tags`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (1000) NOT NULL);

ALTER TABLE `Tags` ADD PRIMARY KEY (Id);




CREATE TABLE `Categories`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (1000) NOT NULL);

ALTER TABLE `Categories` ADD PRIMARY KEY (Id);




CREATE TABLE `Profiles`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FirstName` varchar (1000) NOT NULL, 
	`LastName` varchar (1000) NOT NULL, 
	`ZipCode` varchar (1000) NOT NULL, 
	`BirthDate` datetime NOT NULL, 
	`Gender` varchar (1000) NOT NULL, 
	`Pets` varchar (1000), 
	`Religion` varchar (1000), 
	`Job` varchar (1000), 
	`Education` varchar (1000), 
	`Ethnicity` varchar (1000), 
	`Athleticism` varchar (1000), 
	`SexualOrientation` varchar (1000), 
	`MarriageStatus` varchar (1000), 
	`Children` varchar (1000), 
	`Smokes` varchar (1000), 
	`Drinks` varchar (1000), 
	`Drugs` varchar (1000));

ALTER TABLE `Profiles` ADD PRIMARY KEY (Id);




CREATE TABLE `Posts_Photo`(
	`Caption` varchar (1000) NOT NULL, 
	`AlbumId` int NOT NULL, 
	`Id` int NOT NULL, 
	`ProfilePhotoOf_Id` int NOT NULL);

ALTER TABLE `Posts_Photo` ADD PRIMARY KEY (Id);




CREATE TABLE `Notifications_RequestNotification`(
	`Id` int NOT NULL, 
	`BroRequest_Id` int NOT NULL);

ALTER TABLE `Notifications_RequestNotification` ADD PRIMARY KEY (Id);




CREATE TABLE `Notifications_FirstBump`(
	`UserId` int NOT NULL, 
	`Id` int NOT NULL);

ALTER TABLE `Notifications_FirstBump` ADD PRIMARY KEY (Id);




CREATE TABLE `Notifications_CommentNotification`(
	`Id` int NOT NULL, 
	`Comment_Id` int NOT NULL);

ALTER TABLE `Notifications_CommentNotification` ADD PRIMARY KEY (Id);




CREATE TABLE `Posts_TextPost`(
	`Content` varchar (1000) NOT NULL, 
	`Id` int NOT NULL);

ALTER TABLE `Posts_TextPost` ADD PRIMARY KEY (Id);




CREATE TABLE `CircleUser`(
	`JoinedCircles_Id` int NOT NULL, 
	`Members_Id` int NOT NULL);

ALTER TABLE `CircleUser` ADD PRIMARY KEY (JoinedCircles_Id, Members_Id);




CREATE TABLE `UserUser`(
	`BlockedByBros_Id` int NOT NULL, 
	`BlockedBros_Id` int NOT NULL);

ALTER TABLE `UserUser` ADD PRIMARY KEY (BlockedByBros_Id, BlockedBros_Id);




CREATE TABLE `InterestProfile`(
	`Interests_Id` int NOT NULL, 
	`InterestedProfiles_Id` int NOT NULL);

ALTER TABLE `InterestProfile` ADD PRIMARY KEY (Interests_Id, InterestedProfiles_Id);




CREATE TABLE `ProductTag`(
	`Products_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `ProductTag` ADD PRIMARY KEY (Products_Id, Tags_Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `JoinedCircles_Id` in table 'CircleUser'

ALTER TABLE `CircleUser`
ADD CONSTRAINT `FK_CircleUser_Circle`
    FOREIGN KEY (`JoinedCircles_Id`)
    REFERENCES `Circles`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Members_Id` in table 'CircleUser'

ALTER TABLE `CircleUser`
ADD CONSTRAINT `FK_CircleUser_User`
    FOREIGN KEY (`Members_Id`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleUser_User'

CREATE INDEX `IX_FK_CircleUser_User` 
    ON `CircleUser`
    (`Members_Id`);

-- Creating foreign key on `BlockedByBros_Id` in table 'UserUser'

ALTER TABLE `UserUser`
ADD CONSTRAINT `FK_UserUser_User`
    FOREIGN KEY (`BlockedByBros_Id`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `BlockedBros_Id` in table 'UserUser'

ALTER TABLE `UserUser`
ADD CONSTRAINT `FK_UserUser_User1`
    FOREIGN KEY (`BlockedBros_Id`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_User1'

CREATE INDEX `IX_FK_UserUser_User1` 
    ON `UserUser`
    (`BlockedBros_Id`);

-- Creating foreign key on `Interests_Id` in table 'InterestProfile'

ALTER TABLE `InterestProfile`
ADD CONSTRAINT `FK_InterestProfile_Interest`
    FOREIGN KEY (`Interests_Id`)
    REFERENCES `Interests`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `InterestedProfiles_Id` in table 'InterestProfile'

ALTER TABLE `InterestProfile`
ADD CONSTRAINT `FK_InterestProfile_Profile`
    FOREIGN KEY (`InterestedProfiles_Id`)
    REFERENCES `Profiles`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InterestProfile_Profile'

CREATE INDEX `IX_FK_InterestProfile_Profile` 
    ON `InterestProfile`
    (`InterestedProfiles_Id`);

-- Creating foreign key on `ProfilePhotoOf_Id` in table 'Posts_Photo'

ALTER TABLE `Posts_Photo`
ADD CONSTRAINT `FK_PhotoProfile`
    FOREIGN KEY (`ProfilePhotoOf_Id`)
    REFERENCES `Profiles`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotoProfile'

CREATE INDEX `IX_FK_PhotoProfile` 
    ON `Posts_Photo`
    (`ProfilePhotoOf_Id`);

-- Creating foreign key on `BroRequest_Id` in table 'Notifications_RequestNotification'

ALTER TABLE `Notifications_RequestNotification`
ADD CONSTRAINT `FK_RequestNotificationBroRequest`
    FOREIGN KEY (`BroRequest_Id`)
    REFERENCES `BroRequests`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestNotificationBroRequest'

CREATE INDEX `IX_FK_RequestNotificationBroRequest` 
    ON `Notifications_RequestNotification`
    (`BroRequest_Id`);

-- Creating foreign key on `CategoryId` in table 'Products'

ALTER TABLE `Products`
ADD CONSTRAINT `FK_ProductCategory`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Categories`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'

CREATE INDEX `IX_FK_ProductCategory` 
    ON `Products`
    (`CategoryId`);

-- Creating foreign key on `Products_Id` in table 'ProductTags'

ALTER TABLE `ProductTags`
ADD CONSTRAINT `FK_ProductTag_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Products`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'ProductTags'

ALTER TABLE `ProductTags`
ADD CONSTRAINT `FK_ProductTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tags`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTag_Tag'

CREATE INDEX `IX_FK_ProductTag_Tag` 
    ON `ProductTags`
    (`Tags_Id`);

-- Creating foreign key on `PostId` in table 'Comments'

ALTER TABLE `Comments`
ADD CONSTRAINT `FK_PostComment`
    FOREIGN KEY (`PostId`)
    REFERENCES `Posts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'

CREATE INDEX `IX_FK_PostComment` 
    ON `Comments`
    (`PostId`);

-- Creating foreign key on `UserId` in table 'Posts'

ALTER TABLE `Posts`
ADD CONSTRAINT `FK_UserPost`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPost'

CREATE INDEX `IX_FK_UserPost` 
    ON `Posts`
    (`UserId`);

-- Creating foreign key on `Profile_Id` in table 'Users'

ALTER TABLE `Users`
ADD CONSTRAINT `FK_UserProfile`
    FOREIGN KEY (`Profile_Id`)
    REFERENCES `Profiles`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfile'

CREATE INDEX `IX_FK_UserProfile` 
    ON `Users`
    (`Profile_Id`);

-- Creating foreign key on `UserId` in table 'Messages'

ALTER TABLE `Messages`
ADD CONSTRAINT `FK_MessagesSent`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessagesSent'

CREATE INDEX `IX_FK_MessagesSent` 
    ON `Messages`
    (`UserId`);

-- Creating foreign key on `UserId1` in table 'Messages'

ALTER TABLE `Messages`
ADD CONSTRAINT `FK_MessageUser`
    FOREIGN KEY (`UserId1`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'

CREATE INDEX `IX_FK_MessageUser` 
    ON `Messages`
    (`UserId1`);

-- Creating foreign key on `UserId` in table 'Circles'

ALTER TABLE `Circles`
ADD CONSTRAINT `FK_CircleOwner`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircleOwner'

CREATE INDEX `IX_FK_CircleOwner` 
    ON `Circles`
    (`UserId`);

-- Creating foreign key on `UserId` in table 'Albums'

ALTER TABLE `Albums`
ADD CONSTRAINT `FK_AlbumUser`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumUser'

CREATE INDEX `IX_FK_AlbumUser` 
    ON `Albums`
    (`UserId`);

-- Creating foreign key on `AlbumId` in table 'Posts_Photo'

ALTER TABLE `Posts_Photo`
ADD CONSTRAINT `FK_AlbumPhoto`
    FOREIGN KEY (`AlbumId`)
    REFERENCES `Albums`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumPhoto'

CREATE INDEX `IX_FK_AlbumPhoto` 
    ON `Posts_Photo`
    (`AlbumId`);

-- Creating foreign key on `UserId` in table 'Preferences'

ALTER TABLE `Preferences`
ADD CONSTRAINT `FK_UserPreferences`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPreferences'

CREATE INDEX `IX_FK_UserPreferences` 
    ON `Preferences`
    (`UserId`);

-- Creating foreign key on `UserId` in table 'BroRequests'

ALTER TABLE `BroRequests`
ADD CONSTRAINT `FK_SentBroRequest`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentBroRequest'

CREATE INDEX `IX_FK_SentBroRequest` 
    ON `BroRequests`
    (`UserId`);

-- Creating foreign key on `UserId1` in table 'BroRequests'

ALTER TABLE `BroRequests`
ADD CONSTRAINT `FK_BroRequestUser`
    FOREIGN KEY (`UserId1`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BroRequestUser'

CREATE INDEX `IX_FK_BroRequestUser` 
    ON `BroRequests`
    (`UserId1`);

-- Creating foreign key on `UserId` in table 'Notifications_FirstBump'

ALTER TABLE `Notifications_FirstBump`
ADD CONSTRAINT `FK_SentFirstBump`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentFirstBump'

CREATE INDEX `IX_FK_SentFirstBump` 
    ON `Notifications_FirstBump`
    (`UserId`);

-- Creating foreign key on `Comment_Id` in table 'Notifications_CommentNotification'

ALTER TABLE `Notifications_CommentNotification`
ADD CONSTRAINT `FK_CommentNotificationComment`
    FOREIGN KEY (`Comment_Id`)
    REFERENCES `Comments`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentNotificationComment'

CREATE INDEX `IX_FK_CommentNotificationComment` 
    ON `Notifications_CommentNotification`
    (`Comment_Id`);

-- Creating foreign key on `UserId1` in table 'Notifications'

ALTER TABLE `Notifications`
ADD CONSTRAINT `FK_NotificationUser`
    FOREIGN KEY (`UserId1`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationUser'

CREATE INDEX `IX_FK_NotificationUser` 
    ON `Notifications`
    (`UserId1`);

-- Creating foreign key on `UserId` in table 'Comments'

ALTER TABLE `Comments`
ADD CONSTRAINT `FK_CommentUser`
    FOREIGN KEY (`UserId`)
    REFERENCES `Users`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUser'

CREATE INDEX `IX_FK_CommentUser` 
    ON `Comments`
    (`UserId`);

-- Creating foreign key on `Id` in table 'Posts_Photo'

ALTER TABLE `Posts_Photo`
ADD CONSTRAINT `FK_Photo_inherits_Post`
    FOREIGN KEY (`Id`)
    REFERENCES `Posts`
        (`Id`)
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating foreign key on `Id` in table 'Notifications_RequestNotification'

ALTER TABLE `Notifications_RequestNotification`
ADD CONSTRAINT `FK_RequestNotification_inherits_Notification`
    FOREIGN KEY (`Id`)
    REFERENCES `Notifications`
        (`Id`)
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating foreign key on `Id` in table 'Notifications_FirstBump'

ALTER TABLE `Notifications_FirstBump`
ADD CONSTRAINT `FK_FirstBump_inherits_Notification`
    FOREIGN KEY (`Id`)
    REFERENCES `Notifications`
        (`Id`)
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating foreign key on `Id` in table 'Notifications_CommentNotification'

ALTER TABLE `Notifications_CommentNotification`
ADD CONSTRAINT `FK_CommentNotification_inherits_Notification`
    FOREIGN KEY (`Id`)
    REFERENCES `Notifications`
        (`Id`)
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating foreign key on `Id` in table 'Posts_TextPost'

ALTER TABLE `Posts_TextPost`
ADD CONSTRAINT `FK_TextPost_inherits_Post`
    FOREIGN KEY (`Id`)
    REFERENCES `Posts`
        (`Id`)
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
