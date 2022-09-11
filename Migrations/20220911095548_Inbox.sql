START TRANSACTION;

ALTER TABLE `Members` MODIFY COLUMN `PhoneNumber` varchar(16) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Members` ADD `DeviceId` varchar(128) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Members` ADD `DeviceManifacturer` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Members` ADD `DeviceModel` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Members` ADD `OneTimeCode` varchar(6) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `DirectMessages` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `InboxID` int NOT NULL,
    `FromMemberID` int NULL,
    `ToMemberID` int NULL,
    `Message` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
    `MessageDate` datetime NOT NULL,
    CONSTRAINT `PK_DirectMessages` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_DirectMessages_Members_FromMemberID` FOREIGN KEY (`FromMemberID`) REFERENCES `Members` (`ID`),
    CONSTRAINT `FK_DirectMessages_Members_ToMemberID` FOREIGN KEY (`ToMemberID`) REFERENCES `Members` (`ID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Inbox` (
    `ID` int NOT NULL AUTO_INCREMENT,
    `FromMemberID` int NULL,
    `ToMemberID` int NULL,
    `CreateDate` datetime NOT NULL,
    `UpdateDate` datetime NOT NULL,
    `UnreadMessageCount` int NOT NULL,
    `LastMessageID` int NULL,
    CONSTRAINT `PK_Inbox` PRIMARY KEY (`ID`),
    CONSTRAINT `FK_Inbox_DirectMessages_LastMessageID` FOREIGN KEY (`LastMessageID`) REFERENCES `DirectMessages` (`ID`),
    CONSTRAINT `FK_Inbox_Members_FromMemberID` FOREIGN KEY (`FromMemberID`) REFERENCES `Members` (`ID`),
    CONSTRAINT `FK_Inbox_Members_ToMemberID` FOREIGN KEY (`ToMemberID`) REFERENCES `Members` (`ID`)
) CHARACTER SET=utf8mb4;

UPDATE `Users` SET `Password` = '$IOPSSWD$V1$10000$0c7NyCRjCViMZqrnHJi+zZGqTbYxsNxR4I/eT9G+xgxxVYB9CI66AGMYMFsGyij/N1HOBwzUiDqI00MD/JmcX+9tH6xcTWXiYU6dLexnFRmqt6wXIs2QSGwe+sAWFQ95'
WHERE `ID` = 1;
SELECT ROW_COUNT();


CREATE INDEX `IX_Members_PhoneNumber` ON `Members` (`PhoneNumber`);

CREATE INDEX `IX_DirectMessages_FromMemberID` ON `DirectMessages` (`FromMemberID`);

CREATE INDEX `IX_DirectMessages_InboxID` ON `DirectMessages` (`InboxID`);

CREATE INDEX `IX_DirectMessages_MessageDate` ON `DirectMessages` (`MessageDate`);

CREATE INDEX `IX_DirectMessages_ToMemberID` ON `DirectMessages` (`ToMemberID`);

CREATE INDEX `IX_Inbox_FromMemberID` ON `Inbox` (`FromMemberID`);

CREATE INDEX `IX_Inbox_LastMessageID` ON `Inbox` (`LastMessageID`);

CREATE INDEX `IX_Inbox_ToMemberID` ON `Inbox` (`ToMemberID`);

CREATE INDEX `IX_Inbox_UpdateDate` ON `Inbox` (`UpdateDate`);

ALTER TABLE `DirectMessages` ADD CONSTRAINT `FK_DirectMessages_Inbox_InboxID` FOREIGN KEY (`InboxID`) REFERENCES `Inbox` (`ID`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220911095548_Inbox', '6.0.2');

COMMIT;

