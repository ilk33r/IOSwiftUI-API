CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Clients` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `ClientId` varchar(48) CHARACTER SET utf8mb4 NULL,
    `ClientSecret` varchar(48) CHARACTER SET utf8mb4 NULL,
    `ClientDescription` longtext CHARACTER SET utf8mb4 NULL,
    `IsEnabled` int NOT NULL,
    `RequestCount` bigint NOT NULL,
    `MaxRequestCount` bigint NOT NULL,
    CONSTRAINT `PK_Clients` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Configurations` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `ConfigKey` varchar(128) CHARACTER SET utf8mb4 NULL,
    `ConfigIntValue` int NULL,
    `ConfigStringValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Configurations` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Images` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FileName` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `FileType` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Width` int NULL,
    `Height` int NULL,
    `Scale` int NULL,
    CONSTRAINT `PK_Images` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Menu` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `Action` varchar(255) CHARACTER SET utf8mb4 NULL,
    `CssClass` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NULL,
    `MenuOrder` int NOT NULL,
    `RequiredRole` int NOT NULL,
    `ParentEntityID` int NULL,
    CONSTRAINT `PK_Menu` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Messages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `Message` longtext CHARACTER SET utf8mb4 NULL,
    `MessageCreateDate` datetime NOT NULL,
    `MessageStartDate` datetime NOT NULL,
    `MessageEndDate` datetime NOT NULL,
    CONSTRAINT `PK_Messages` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `UserName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Password` longtext CHARACTER SET utf8mb4 NULL,
    `UserRole` int NOT NULL,
    `UserToken` varchar(36) CHARACTER SET utf8mb4 NULL,
    `TokenDate` datetime NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PushNotifications` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `ClientID` int NULL,
    `AppBuildNumber` int NOT NULL,
    `AppBundleId` varchar(64) CHARACTER SET utf8mb4 NULL,
    `AppVersion` varchar(10) CHARACTER SET utf8mb4 NULL,
    `BadgeCount` int NOT NULL,
    `DeviceId` varchar(128) CHARACTER SET utf8mb4 NULL,
    `DeviceName` varchar(128) CHARACTER SET utf8mb4 NULL,
    `DeviceToken` varchar(512) CHARACTER SET utf8mb4 NULL,
    `DeviceType` int NOT NULL,
    `LastUpdateTime` datetime NOT NULL,
    CONSTRAINT `PK_PushNotifications` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_PushNotifications_Clients_ClientID` FOREIGN KEY (`ClientID`) REFERENCES `Clients` (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PushNotificationMessages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `ClientID` int NULL,
    `DeviceType` int NOT NULL,
    `NotificationCategory` varchar(64) CHARACTER SET utf8mb4 NULL,
    `NotificationData` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NotificationDate` datetime NOT NULL,
    `NotificationMessage` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NotificationTitle` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsCompleted` int NOT NULL,
    `PushNotificationDeviceIDID` int NULL,
    CONSTRAINT `PK_PushNotificationMessages` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_PushNotificationMessages_Clients_ClientID` FOREIGN KEY (`ClientID`) REFERENCES `Clients` (`ID`),
    CONSTRAINT `FK_PushNotificationMessages_PushNotifications_PushNotificationD~` FOREIGN KEY (`PushNotificationDeviceIDID`) REFERENCES `PushNotifications` (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PushNotificationDeliveredMessages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `PushNotificationID` int NULL,
    `PushNotificationMessageID` int NULL,
    CONSTRAINT `PK_PushNotificationDeliveredMessages` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_PushNotificationDeliveredMessages_PushNotificationMessages_P~` FOREIGN KEY (`PushNotificationMessageID`) REFERENCES `PushNotificationMessages` (`ID`),
    CONSTRAINT `FK_PushNotificationDeliveredMessages_PushNotifications_PushNoti~` FOREIGN KEY (`PushNotificationID`) REFERENCES `PushNotifications` (`ID`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Configurations` (`ID`, `ConfigIntValue`, `ConfigKey`, `ConfigStringValue`)
VALUES (1, 0, 'IsMaintenanceModeOn', NULL);
INSERT INTO `Configurations` (`ID`, `ConfigIntValue`, `ConfigKey`, `ConfigStringValue`)
VALUES (2, 0, 'ForgotPasswordEmailTitle', 'IOBootstrapt Reset Password');
INSERT INTO `Configurations` (`ID`, `ConfigIntValue`, `ConfigKey`, `ConfigStringValue`)
VALUES (3, 0, 'ForgotPasswordEmailHtmlBody', CONCAT('<p>This email has been sent upon your ''Change My Password'' request. If you don''t have such request please ignore this email.', CHAR(10), 'Please click to the link to  <a href="{0}">Change your password.</a></p>', CHAR(10), '<br />', CHAR(10), '<p>Best Regards, IOBootstrapt</p>'));
INSERT INTO `Configurations` (`ID`, `ConfigIntValue`, `ConfigKey`, `ConfigStringValue`)
VALUES (4, 0, 'ForgotPasswordEmailTextBody', CONCAT('This email has been sent upon your ''Change My Password'' request. If you don''t have such request please ignore this email.', CHAR(10), 'Please click to the link to  {0}', CHAR(10), '', CHAR(10), 'Best Regards, IOBootstrapt'));
INSERT INTO `Configurations` (`ID`, `ConfigIntValue`, `ConfigKey`, `ConfigStringValue`)
VALUES (5, 0, 'EMailFromName', 'IOBootstrapt Support');

INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (1, 'actionClients', 'fa-cloud', 1, 'Clients', NULL, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (2, 'clientsList', 'fa-circle-o', 2, 'List Clients', 1, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (3, 'clientsAdd', 'fa-circle-o', 3, 'Add Client', 1, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (6, 'actionUsers', 'fa-users', 6, 'Users', NULL, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (7, 'usersList', 'fa-circle-o', 7, 'List Users', 6, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (8, 'usersAdd', 'fa-circle-o', 8, 'Add User', 6, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (11, 'actionConfiguration', 'fa-wrench', 11, 'Configurations', NULL, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (12, 'configurationsList', 'fa-circle-o', 12, 'Edit Configurations', 11, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (13, 'configurationsAdd', 'fa-circle-o', 13, 'Add Configuration', 11, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (14, 'resetCache', 'fa-circle-o', 14, 'Reset Cache', 11, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (15, 'actionMenuEditor', 'fa-list', 15, 'Menu Editor', NULL, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (16, 'menuEditorList', 'fa-circle-o', 16, 'List Menu Items', 15, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (17, 'menuEditorAdd', 'fa-circle-o', 17, 'Add Menu Item', 15, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (18, 'actionMessages', 'fa-envelope', 18, 'Messages', NULL, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (19, 'messagesList', 'fa-circle-o', 19, 'List Messages', 18, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (20, 'messagesAdd', 'fa-circle-o', 20, 'Add Message', 18, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (21, 'actionPushNotification', 'fa-comment-alt', 21, 'Push Notifications', NULL, 2);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (22, 'pushNotificationList', 'fa-circle-o', 22, 'List Messages', 21, 2);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (23, 'pushNotificationSend', 'fa-circle-o', 23, 'Send', 21, 2);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (24, 'actionResource', 'fa-address-book', 24, 'Resources', NULL, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (25, 'resourcesList', 'fa-circle-o', 25, 'Edit Resources', 24, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (26, 'resourceAdd', 'fa-circle-o', 26, 'Add Resource', 24, 0);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (27, 'actionImages', 'fa-image', 27, 'Images', NULL, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (28, 'imagesEdit', 'fa-circle-o', 28, 'Edit Images', 27, 1);
INSERT INTO `Menu` (`ID`, `Action`, `CssClass`, `MenuOrder`, `Name`, `ParentEntityID`, `RequiredRole`)
VALUES (29, 'imageAdd', 'fa-circle-o', 29, 'Add Image', 27, 1);

INSERT INTO `Users` (`ID`, `Password`, `TokenDate`, `UserName`, `UserRole`, `UserToken`)
VALUES (1, '$IOPSSWD$V1$10000$ZgNuJbQUP1x45jSv12s+Z2jMz3WMMo7cibs8kaLi01TC60uUxa3OMSGIxTWrVw+m0tlxGoAMslGZxh7dS4DMuVbPzI+ljOzXJ+zUj/wntrmbTqo4NSiDjCxdZCFgrQPH', TIMESTAMP '0001-01-01 00:00:00', 'root', 0, '');

CREATE UNIQUE INDEX `IX_Clients_ClientId` ON `Clients` (`ClientId`);

CREATE UNIQUE INDEX `IX_Configurations_ConfigKey` ON `Configurations` (`ConfigKey`);

CREATE INDEX `IX_Menu_ParentEntityID_MenuOrder_RequiredRole` ON `Menu` (`ParentEntityID`, `MenuOrder`, `RequiredRole`);

CREATE INDEX `IX_Messages_MessageCreateDate_MessageEndDate_MessageStartDate` ON `Messages` (`MessageCreateDate`, `MessageEndDate`, `MessageStartDate`);

CREATE INDEX `IX_PushNotificationDeliveredMessages_PushNotificationID` ON `PushNotificationDeliveredMessages` (`PushNotificationID`);

CREATE INDEX `IX_PushNotificationDeliveredMessages_PushNotificationMessageID` ON `PushNotificationDeliveredMessages` (`PushNotificationMessageID`);

CREATE INDEX `IX_PushNotificationMessages_ClientID` ON `PushNotificationMessages` (`ClientID`);

CREATE INDEX `IX_PushNotificationMessages_NotificationDate_DeviceType_IsCompl~` ON `PushNotificationMessages` (`NotificationDate`, `DeviceType`, `IsCompleted`);

CREATE INDEX `IX_PushNotificationMessages_PushNotificationDeviceIDID` ON `PushNotificationMessages` (`PushNotificationDeviceIDID`);

CREATE INDEX `IX_PushNotifications_ClientID` ON `PushNotifications` (`ClientID`);

CREATE INDEX `IX_PushNotifications_DeviceId_DeviceType_LastUpdateTime` ON `PushNotifications` (`DeviceId`, `DeviceType`, `LastUpdateTime`);

CREATE UNIQUE INDEX `IX_Users_UserName` ON `Users` (`UserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220903124747_Initial', '6.0.2');

COMMIT;

